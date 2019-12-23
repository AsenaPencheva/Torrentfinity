namespace TorrentFinity.DynamicModules.Tests.Torrents
{
    using Moq;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using Telerik.Sitefinity.Data;
    using Telerik.Sitefinity.DynamicModules;
    using Torrentfinity.Mvc.Models;
    using Torrentfinity.Sitefinity.Common.Providers;
    using Torrentfinity.Sitefinity.Services.DynamicModules.BuldInContents;
    using Torrentfinity.Sitefinity.Services.DynamicModules.Torrents;

    [TestFixture]
    public class TorrentsServiceTests
    {
        //[Test]
        //public void CreateTorrentShould_ThrowArgumentNullException_WhenPassedModel_IsNull()
        //{
        //    // Arrange
        //    var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
        //    var mockedimagesService = new Mock<IImagesService>();
        //    var mockedManagerProvider = new Mock<IManagerProvider>();
        //    var sut = new TorrentsService(mockedimagesService.Object, mockedDateTimeProvider.Object, mockedManagerProvider.Object);

        //    // Act
        //    // Assert
        //    Assert.Throws<ArgumentNullException>(() => sut.CreateTorrent(null));
        //}

        //[Test]
        //public void CreateTorrentShould_ThrowArgumentException_WhenTorrent_Exists()
        //{
        //    // Arrange
        //    var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
        //    var mockedimagesService = new Mock<IImagesService>();
        //    var mockedManagerProvider = new Mock<IManagerProvider>();
        //    var mockManager = new Mock<ManagerBase<DynamicModuleDataProvider>>();

        //    mockedManagerProvider.Setup(x => x.ResolveType(It.IsAny<string>())).Returns(It.IsAny<Type>);

        //    mockedManagerProvider.Setup(x => x.GetDynamicModuleManager(It.IsAny<string>(), It.IsAny<string>())).Returns(mockManager.Object);
        //    var model = new TorrentViewModel
        //    {
        //        Genre = "Genre",
        //        LanguageContents = new List<LanguageContents> { new LanguageContents { AdditionalInfo = "test", Language = "en", Title = "existig title" } },
        //        DownloadLink = "DownloadLink"
        //    };

        //    var sut = new TorrentsService(mockedimagesService.Object, mockedDateTimeProvider.Object, mockedManagerProvider.Object);

        //    // Act

        //    // Assert
        //    Assert.Throws<ArgumentNullException>(() => sut.CreateTorrent(null));
        //}
    }
}
