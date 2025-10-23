# 🎓 Student API (Trail Workday Task)

This project is a .NET 8 Web API built with **FastEndpoints** for managing students, classes, enrollments, and marks using in-memory data storage.

---

## 🚀 Features

### Students
- Create Student — `POST /api/students`
- Get All Students (with pagination & filters) — `GET /api/students`
- Update Student — `PUT /api/students/{id}`
- Delete Student — `DELETE /api/students/{id}`
- Generate Student Report — `GET /api/students/{studentId}/report`

### Classes
- Create Class — `POST /api/classes`
- Get All Classes (with pagination & filters) — `GET /api/classes`
- Delete Class — `DELETE /api/classes/{id}`
- Get Class Average Marks — `GET /api/classes/{classId}/average-marks`

### Enrollments
- Enroll Student in Class — `POST /api/enrollments`

### Marks
- Record Marks — `POST /api/marks`

---

## 🧠 Tech Stack
- .NET 8 / FastEndpoints
- In-memory data storage
- FluentValidation
- Swagger UI for testing

---

## 🏁 Run Instructions


