using NUnit.Framework;
using Moq;
using ExposureLog.ViewModels;
using ExposureLog.Services;
using ExposureLog.Models;

namespace ExposureLog.Tests.ViewModels
{
    [TestFixture]
    public class DetailViewModelTests
    {
        private DetailViewModel _vm;

        [SetUp]
        public void Setup()
        {
            var navMock = new Mock<INavService>().Object;
            var analyticsMock = new Mock<IAnalyticsService>().Object;
            _vm = new DetailViewModel(navMock, analyticsMock);
        }

        [Test]
        public void EntryIsSetToParameterProvidedOnInit()
        {
            var mockEntry = new Mock<ExposureLogEntry>().Object;
            _vm.Entry = null;

            _vm.Init(mockEntry);

            Assert.IsNotNull(_vm.Entry, "Entry is still null after being initialized with a valid ExposureLogEntry object");
        }

        [Test]
        public void InitThrowsWhenNoParameterIsProvided()
        {
            Assert.Throws(typeof(EntryNotProvidedException), () => _vm.Init());
        }
    }
}
