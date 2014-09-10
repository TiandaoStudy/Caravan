using System.Collections.Generic;
using System.IO;
using FLEX.Common;
using FLEX.Common.Web;
using PommaLabs.GRAMPA;

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
            new QueryExecutor().ExecuteStoredProcedure("ResetDatabase");
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

        private static IEnumerable<Pair<int, int>> TestCandidatesSchools()
        {
            return new[]
            {
                Pair.Create(0, 6), // Davide Paparella, Scuola Media Inferiore
                Pair.Create(1, 6), // Diana Martinez Gonzalez, Scuola Media InferioreCorso di Laurea in Informatica
                Pair.Create(1, 0), // Diana Martinez Gonzalez, Corso di Laurea in Informatica
            };
        }

        private static IEnumerable<Pair<int, int>> TestCandidatesGender()
        {
            return new[]
            {
                Pair.Create(0, 1), // Davide Paparella, Male
                Pair.Create(1, 0), // Diana Martinez Gonzalez, Female
            };
        }
    }
}
