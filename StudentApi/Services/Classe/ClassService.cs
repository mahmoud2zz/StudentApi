using System;
using StudentApi.Models;
using System.Collections.Concurrent;

namespace StudentApi.Services.Classe
{
	public class ClassService:IClassService
	{
        private readonly ConcurrentDictionary<int, Class> _classes = new();
        private int _nextId = 1;

        public Class AddClass(Class newClass)
        {
            newClass.Id = _nextId++;
            _classes[newClass.Id] = newClass;
            return newClass;
        }

        public List<Class> GetAllClasses()
        {
            return _classes.Values.ToList();
        }

        public bool DeleteClass(int id)
        {
            return _classes.TryRemove(id, out _);
        }

        public Class? GetClassById(int id)
        {
            _classes.TryGetValue(id, out var classObj);
            return classObj;
        }

    }
}

