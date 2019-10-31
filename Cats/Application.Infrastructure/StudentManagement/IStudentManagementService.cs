﻿using System.Collections.Generic;
using Application.Core.Data;
using LMP.Models;

namespace Application.Infrastructure.StudentManagement
{
    public interface IStudentManagementService
    {
        Student GetStudent(int userId);

        IEnumerable<Student> GetGroupStudents(int groupId);

        IEnumerable<Student> GetStudents();

        IPageableList<Student> GetStudentsPageable(string searchString = null, IPageInfo pageInfo = null, IEnumerable<ISortCriteria> sortCriterias = null);

        Student Save(Student student);

        void UpdateStudent(Student student);

        bool DeleteStudent(int id);

	    int CountUnconfirmedStudents(int lecturerId);

	    void СonfirmationStudent(int studentId);

	    void UnConfirmationStudent(int studentId);
    }
}