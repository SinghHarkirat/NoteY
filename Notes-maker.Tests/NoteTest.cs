using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Notes_maker.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Notes_maker.Tests
{

    public class NoteTest
    {

        public NoteTest()
        {

        }
        [Fact]
        public void Get_ShouldReturn_OkResult()
        {
            //Arrange
            var repoMock = Mock.Of<INoteRepo>();
            var noteController = new NotesController(repoMock);
            //Act
            var result = noteController.Get();
            //Assert
            Assert.IsType<OkObjectResult>(result);

        }
        [Fact]
        public void Get_Should_MatchResult()
        {
            //Arrange
            var repoMock = new Mock<INoteRepo>();
            var noteController = new NotesController(repoMock.Object);
            List<Note> mockList = new List<Note>();
            repoMock.Setup(x => x.GetAllNotes()).Returns(mockList);

            //Act
            var data = noteController.Get();

            //Assert
            var okResult = data.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeAssignableTo<List<Note>>();

        }
        [Fact]
        public void GetNote_ShouldReturn_OkResultIfNoteFound()
        {
            //Arrange
            var repoMock = new Mock<INoteRepo>();
            var noteController = new NotesController(repoMock.Object);
            Note item = new Note();
            repoMock.Setup(x => x.GetNoteById(It.IsAny<int>())).Returns(item);

            //Act
            var result = noteController.GetNote(1);

            //Assert
            Assert.NotNull(item);
            Assert.IsType<OkObjectResult>(result);


        }

        [Fact]
        public void GetNote_ShouldReturn_NotFoundIfNoteIsNull()
        {
            //Arrange
            var repoMock = new Mock<INoteRepo>();
            var noteController = new NotesController(repoMock.Object);

            //Act
            var result = noteController.GetNote(50);

            //Assert
            Assert.IsType<NotFoundResult>(result);

        }
        [Fact]
        public void GetNote_Should_MatchResult()
        {
            //Arrange
            var repoMock = new Mock<INoteRepo>();
            var noteController = new NotesController(repoMock.Object);
            Note note = new Note() { Id = 1, Name = "acb" };
            repoMock.Setup(x => x.GetNoteById(It.IsAny<int>())).Returns(note);

            //Act
            var result=noteController.GetNote(1);

            //Assert
            Assert.NotNull(result);
            var okResult=result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(note);

        }
        
        [Fact]
        public void AddNote_Should_ReturnCreatedResult()
        {   
            //Arrange
            var repoMock = new Mock<INoteRepo>();
            var noteController = new NotesController(repoMock.Object);
            Note newNote = new Note() { Name = "xyz" };
            //Act
            
            var context = new ValidationContext(newNote, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(newNote, context, results, true);
            var result = noteController.AddNote(newNote);

            //Assert
            Assert.True(isModelStateValid);
            repoMock.Verify(x => x.InsertNote(newNote), Times.Once);
            Assert.IsType<CreatedAtActionResult>(result);

        }

        [Fact]
        public void AddNote_Should_ReturnBadRequest()
        {
            //Arrange
            var repoMock = new Mock<INoteRepo>();
            var noteController = new NotesController(repoMock.Object);
            Note newNote = new Note() { Name = "xyz" };
            //Act

            noteController.ModelState.AddModelError("test", "test");
            var result = noteController.AddNote(newNote);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);

        }

        [Fact]
        public void DeleteNote_Should_ReturnNotFound()
        {
            //Arrange
            var mockRepo = new Mock<INoteRepo>();
            var noteController = new NotesController(mockRepo.Object);

            //Act
            var result=noteController.DeleteNote(50);

            //Assert
            mockRepo.Verify(x => x.GetNoteById(It.IsAny<int>()), Times.Once);
            Assert.IsType<NotFoundResult>(result);
        }
        
        [Fact]
        public void DeleteNote_Should_ReturnNoContent()
        {

            //Arrange
            var mockRepo = new Mock<INoteRepo>();
            var noteController = new NotesController(mockRepo.Object);
            var noteMock=Mock.Of<Note>();
            mockRepo.Setup(x => x.GetNoteById(It.IsAny<int>())).Returns(noteMock);

            //Act
            var result = noteController.DeleteNote(50);

            //Assert
            mockRepo.Verify(x => x.DeleteNote(It.IsAny<int>()), Times.Once);
            Assert.IsType<NoContentResult>(result);

        }
    }
}
