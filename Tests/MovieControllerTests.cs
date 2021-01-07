using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using OOP_Projektarbete_Backend.Controllers;
using OOP_Projektarbete_Backend.Helpers;
using OOP_Projektarbete_Backend.Models;

using System.Threading.Tasks;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Popular_RetunsActionResult_Ok()
        {
            //Arange
            var mockRepo = new Mock<IMovieRepository>();
            mockRepo.Setup(x => x.GetPopularMovies("1"))
                .ReturnsAsync(GetSampleData());
            var controller = new MovieController(mockRepo.Object);

            //Act
            var respsonse = await controller.Popular("1");
            var actual = ToObjectResult(respsonse);
            var expected = GetSampleData();

            //Assert
            Assert.AreEqual(expected.Page, actual.Page);
            Assert.AreEqual(expected.Results.Length, actual.Results.Length);
            Assert.AreEqual(expected.Total_pages, actual.Total_pages);
            Assert.AreEqual(expected.Total_results, actual.Total_results);
            Assert.IsNotNull(actual);
        }

        private MovieInfo GetSampleData()
        {
            return new MovieInfo()
            {
                Page = 1,
                Results = new MovieInfoResult[] { new MovieInfoResult() { Title = "Spiderman" } },
                Total_pages = 100,
                Total_results = 500
            };
        }

        public static MovieInfo ToObjectResult(IActionResult actionResult)
        {
            var result = actionResult as OkObjectResult;

            return (MovieInfo)result.Value;
        }
    }
}