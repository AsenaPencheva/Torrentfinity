namespace TorrentFinity.DynamicModules.Tests.Torrents
{
    using Moq;
    using NUnit.Framework;
    using System;
    using Torrentfinity.Sitefinity.Common.Providers;
    using Torrentfinity.Sitefinity.Services.DynamicModules.BuldInContents;
    using Torrentfinity.Sitefinity.Services.DynamicModules.Torrents;

    [TestFixture]
    public class TorrentsServiceTests
    {
        [Test]
        public void CreateTorrentShould_ThrowArgumentNullException_WhenPassedModel_IsNull()
        {
            // Arrange
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockedimagesService = new Mock<IImagesService>();
            var mockedManagerProvider = new Mock<IManagerProvider>();
            var sut = new TorrentsService(mockedimagesService.Object, mockedDateTimeProvider.Object, mockedManagerProvider.Object);

            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => sut.CreateTorrent(null));
        }
    }
}
