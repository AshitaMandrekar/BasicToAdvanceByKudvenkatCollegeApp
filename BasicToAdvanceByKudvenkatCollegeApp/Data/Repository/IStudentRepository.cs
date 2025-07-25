namespace BasicToAdvanceByKudvenkatCollegeApp.Data.Repository
{
    public interface IStudentRepository 
    {
       Task<List<Student>> GetAllStudents();
        Task<Student> GetStudentByIdAsync(int id, bool useTracking = false);
        Task<Student> GetStudentByNameAsync(string name);
        Task<int> CreateAsync(Student student);

        Task<int> updateAsync(Student student);

        Task<int> updatePatch(Student student);
        Task<bool> DeleteStudentbyIdAsync(Student student);
    }
}
