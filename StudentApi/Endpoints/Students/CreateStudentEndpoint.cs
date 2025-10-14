using System;
using FastEndpoints;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using StudentApi.Dtos;
using StudentApi.Helpers;
using StudentApi.Models;
using StudentApi.Validators;

namespace StudentApi.Endpoints.Students
{
	public class CreateStudentEndpoint :Endpoint<StudentDto>
	{
        private readonly IStudentService _studentService;

        public CreateStudentEndpoint(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public override void Configure()
        {
            Post("/api/students");
            AllowAnonymous();
        }

        public override async Task HandleAsync( StudentDto req, CancellationToken ct)
        {
            var validator = new StudentValidator();
            var validationResult = validator.Validate(req);

            if (!validationResult.IsValid)
            {
                var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                await SendAsync(ResponseBuilder.Failure<Student>(errors), 400, ct);
                return;
            }

            var student = new Student
            {
                FirstName = req.FirstName,
                LastName = req.LastName,
                Age = req.Age
            };

            var result = _studentService.AddStudent(student);
            await SendAsync(ResponseBuilder.Success("Student created successfully", result), 200, ct);

        }

    }
}

