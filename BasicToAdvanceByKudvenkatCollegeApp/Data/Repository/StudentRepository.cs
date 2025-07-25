
using Microsoft.EntityFrameworkCore;

namespace BasicToAdvanceByKudvenkatCollegeApp.Data.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly CollegeDBContext _dbContext;
        public StudentRepository(CollegeDBContext dbContext) // 
        {
            _dbContext = dbContext; // DI 
        }
        public async Task<int> CreateAsync(Student student)
        {
            _dbContext.Students.Add(student);
             await  _dbContext.SaveChangesAsync();
            return student.Id;
        }

        public async Task<bool> DeleteStudentbyIdAsync(Student student)
        {
            //var dt = await _dbContext.Students.Where(x => x.Id == id).FirstOrDefaultAsync();// using EF - part 43 
            //if(dt==null)
            //{

            //}
            _dbContext.Students.Remove(student);
           await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Student>> GetAllStudents()
        {
           return await _dbContext.Students.ToListAsync();

        }

        public async Task<Student> GetStudentByIdAsync(int id, bool useNoTracking = false)
        {
            if(useNoTracking)
               return await _dbContext.Students.AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();
            else
                return await _dbContext.Students.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Student> GetStudentByNameAsync(string name)
        {
            var students = await _dbContext.Students.Where(x => x.StudentName.ToLower().Contains(name.ToLower())).FirstOrDefaultAsync();
            return students;
        }

        public async Task<int> updateAsync(Student student)
        {
            //if(student!=null)
            //{
            //    var studentUpdate = await _dbContext.Students.Where(x => x.Id == student.Id).FirstOrDefaultAsync();
            //    if (studentUpdate != null)
            //    {
            //        studentUpdate.StudentName = student.StudentName;
            //        studentUpdate.Id = student.Id;
            //        studentUpdate.Email = student.Email;
            //        studentUpdate.Address = student.Address;
            //        studentUpdate.DOB = student.DOB;
            //        _dbContext.Students.Update(studentUpdate);
            //        await _dbContext.SaveChangesAsync();
            //    }

            //}
            _dbContext.Update(student);
            await _dbContext.SaveChangesAsync();
            return student.Id;
        }

        public Task<int> updatePatch(Student student)
        {
            throw new NotImplementedException();
        }

        //public Task<int> updatePatchAsync(Student student)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
