﻿using System.Collections.Generic;
using Finsa.Caravan;

namespace FLEX.Extensions.TestDataAccess
{
    public static class Manager
    {
        private const int DefaultId = 0;

        public static void ClearDatabase()
        {
            using (var ctx = new DataContext())
            {
                // Schools
                foreach (var s in ctx.Schools)
                {
                    ctx.DeleteObject(s);
                }
                ctx.SaveChanges();
            }
        }

        public static void ResetDatabase()
        {
            //QueryExecutor.Instance.ExecuteStoredProcedure("ResetDatabase");
        }

        private static School[] TestSchools()
        {
            return new[]
            {
                School.CreateSchool(DefaultId, "Corso di Laurea in Informatica"),
                School.CreateSchool(DefaultId, "Corso di Laurea in Lettere Antiche"),
                School.CreateSchool(DefaultId, "Corso di Laurea in Lettere Moderne"),
                School.CreateSchool(DefaultId, "Liceo Classico"),
                School.CreateSchool(DefaultId, "Liceo Psico Pedagogico"),
                School.CreateSchool(DefaultId, "Liceo Scientifico"),
                School.CreateSchool(DefaultId, "Scuola Media Inferiore"),
            };
        }

        private static Gender[] TestGender()
        {
            return new[]
            {
                Gender.CreateGender(DefaultId, "Female"),
                Gender.CreateGender(DefaultId, "Male")
            };
        }

        private static Candidate[] TestCandidates()
        {
            return new[]
            {
                Candidate.CreateCandidate(DefaultId, "Davide", "Paparella", "davide.paparella@finsa.it"),
                Candidate.CreateCandidate(DefaultId, "Diana", "Martinez Gonzalez", "diana.martinezgonzalez@finsa.it"),
            };
        }

        private static IEnumerable<GPair<int, int>> TestCandidatesSchools()
        {
            return new[]
            {
                GPair.Create(0, 6), // Davide Paparella, Scuola Media Inferiore
                GPair.Create(1, 6), // Diana Martinez Gonzalez, Scuola Media InferioreCorso di Laurea in Informatica
                GPair.Create(1, 0), // Diana Martinez Gonzalez, Corso di Laurea in Informatica
            };
        }

        private static IEnumerable<GPair<int, int>> TestCandidatesGender()
        {
            return new[]
            {
                GPair.Create(0, 1), // Davide Paparella, Male
                GPair.Create(1, 0), // Diana Martinez Gonzalez, Female
            };
        }
    }
}