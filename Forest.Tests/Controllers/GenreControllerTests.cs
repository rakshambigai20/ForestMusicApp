using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using Forest.Controllers;
using Forest.Data.Models.Domain;
using Forest.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace Forest.Tests.Controllers
{
    public class GenreControllerTests
    {
        private IGenreService _genreservice;
        private GenreController _genrecontroller;

        public GenreControllerTests()
        {
            //Dependencies
            _genreservice = A.Fake<IGenreService>();

            //SUT
            _genrecontroller = new GenreController();
        }

        [Fact]
        public void GenreController_GetGenres_ReturnsViewResult()
        {
            // Arrange
            var genres = A.Fake<IList<Genre>>();
            A.CallTo(() => _genreservice.GetGenres()).Returns(genres);
            

            // Act
            var result = _genrecontroller.GetGenres();
            // Assert

            result.Should().BeOfType<ViewResult>();
            result.Should().NotBeNull();
            //result.Should().BeEquivalentTo(genres);
            //result.Should().GetType().Should().BeAssignableTo<IList<Genre>>();
        }

        [Fact]
        public void GenreController_GetGenre_ReturnsViewResult()
        {
            //Arrange
            var id = 1;
            var genre = A.Fake<Genre>();
            A.CallTo(() => _genreservice.GetGenre(id)).Returns(genre);
            
            //Act

            var result = _genrecontroller.GetGenre(id);
            
            //Assert
            result.Should().BeOfType<ViewResult>();

        }
    }
}
