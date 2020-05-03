using ExposureLog.Models;
using ExposureLog.Services;
using ExposureLog.ViewModels;
using Moq;
using NUnit.Framework;


namespace ExposureLog.Tests.ViewModels
{
    [TestFixture]
    public class NewEntryViewModelTests
    {
        private NewEntryViewModel _vm;
        private Mock<INavService> _navMock;
        private Mock<IExposureLogDataService> _dataMock;
        private Mock<ILocationService> _locMock;

        [SetUp]
        public void Setup()
        {
            _navMock = new Mock<INavService>();
            _dataMock = new Mock<IExposureLogDataService>();
            _locMock = new Mock<ILocationService>();

            _navMock.Setup(x => x.GoBack())
                .Verifiable(); 
            _dataMock.Setup(x => x.AddEntryAsync(It.Is<ExposureLogEntry>(entry => entry.Title == "Mock Entry")))
                .Verifiable();
            _locMock.Setup(x => x.GetCoordinatesAsync())
                .ReturnsAsync(new Coordinates
                {
                    Latitude = 123,
                    Longitude = 321
                });
            _vm = new NewEntryViewModel(_navMock.Object, _locMock.Object, _dataMock.Object);
        }

        [Test]
        public void EntryCoordinatesAreSetOnInit()
        {
            _vm.Latitude = 0.0;
            _vm.Longitude = 0.0;

            _vm.Init();

            Assert.AreEqual(123, _vm.Latitude);
            Assert.AreEqual(321, _vm.Longitude);
        }

        [Test]
        public void CannotExecuteSaveCommandIfTitleIsEmpty()
        {
            _vm.Title = "";

            var canSave = _vm.SaveCommand.CanExecute(null);

            Assert.IsFalse(canSave);
        }

        [Test]
        public void CannotExecuteSaveCommandIfRatingIsOutOfRange()
        {
            _vm.Rating = 6;

            var canSave = _vm.SaveCommand.CanExecute(null);

            Assert.IsFalse(canSave);
        }

        [Test]
        public void SaveCommandAddsEntryToBackend()
        {
            _vm.Title = "Mock Entry";

            _vm.SaveCommand.Execute(null);

            _dataMock.Verify(x => x.AddEntryAsync(It.Is<ExposureLogEntry>(entry => entry.Title == "Mock Entry")), Times.Once);
        }

        [Test]
        public void SaveCommandNavigatesBack()
        {
            _vm.Title = "Mock Entry";

            _vm.SaveCommand.Execute(null);

            _navMock.Verify(x => x.GoBack(), Times.Once);
        }
    }
}
