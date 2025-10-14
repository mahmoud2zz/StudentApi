	using System;
using StudentApi.Models;

namespace StudentApi.Services.Classe
{
	public interface IClassService
	{
        Class AddClass(Class newClass);
        List<Class> GetAllClasses();
        bool DeleteClass(int id);
        public Class? GetClassById(int id);


    }
}

