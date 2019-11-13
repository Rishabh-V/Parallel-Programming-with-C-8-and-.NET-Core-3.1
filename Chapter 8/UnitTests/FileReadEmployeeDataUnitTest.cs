using FileIO;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class FileReadEmployeeDataUnitTest
    {
        [Fact]
        public void EmployeeDetailsEmployeesWithHigherBonusFound()
        {
            //Setup mocking data
            string mockPath = "mockPath";
            StringBuilder content = GetMockFileData();
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(content.ToString()));
            Mock<IFileReader> reader = new Mock<IFileReader>();
            //Using Moq to respond with mock data for file stream
            reader.Setup(sr => sr.GetFileReader(mockPath)).Returns(new StreamReader(ms));
            FileReadFromDisk sut = new FileReadFromDisk(reader.Object);
            //Call the app
            List<Employee> employeesWithHigherBonus = sut.ReadFileandProcessTask(mockPath, 200000);
            //Assert
            Assert.NotNull(employeesWithHigherBonus);
            Assert.Equal(2, employeesWithHigherBonus.Count);
        }

        [Fact]
        public void EmployeeDetailsEmployeesWithHigherBonusNotFound()
        {
            //Setup mocking data
            string mockPath = "mockPath";
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(String.Empty));
            Mock<IFileReader> reader = new Mock<IFileReader>();
            //Using Moq to respond with mock data for file stream
            reader.Setup(sr => sr.GetFileReader(mockPath)).Returns(new StreamReader(ms));
            FileReadFromDisk sut = new FileReadFromDisk(reader.Object);
            //Call the app
            List<Employee> employeesWithHigherBonus = sut.ReadFileandProcessTask(mockPath, 0);
            //Assert
            Assert.Empty(employeesWithHigherBonus);
        }

        [Fact]
        public void EmployeeDetailsProcessingFailed()
        {
            //Setup mocking data
            StringBuilder mockFileExceptionData = new StringBuilder();
            // Record to throw exception
            mockFileExceptionData.AppendLine("Exception record");
            mockFileExceptionData.AppendLine("1	Employee id -1	 Bonus -14392");
            string mockPath = "mockPath";
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(mockFileExceptionData.ToString()));
            Mock<IFileReader> reader = new Mock<IFileReader>();
            reader.Setup(sr => sr.GetFileReader(mockPath)).Returns(new StreamReader(ms));
            //Call the app
            FileReadFromDisk sut = new FileReadFromDisk(reader.Object);
            //Assert
            var ex = Assert.Throws<AggregateException>(() => sut.ReadFileandProcessTask(mockPath, 10000));
            Assert.Single(ex.InnerExceptions);
            //Asserting inner exception
            Assert.Equal((new IndexOutOfRangeException()).GetType().Name, ex.Flatten().InnerExceptions[0].GetType().Name);
        }

        private StringBuilder GetMockFileData()
        {
            StringBuilder mockFileData = new StringBuilder();
            mockFileData.AppendLine("1	Employee id -1	 Bonus -14392");
            mockFileData.AppendLine("2	Employee id -2	 Bonus -130140");
            mockFileData.AppendLine("3	Employee id -3	 Bonus -177949");
            mockFileData.AppendLine("4	Employee id -4	 Bonus -86477");
            mockFileData.AppendLine("5	Employee id -5	 Bonus -202725");
            mockFileData.AppendLine("6	Employee id -6	 Bonus -203595");
            mockFileData.AppendLine("7	Employee id -7	 Bonus -43698");
            return mockFileData;
        }
    }
}
