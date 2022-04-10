using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;
using TMDB_demo.Models.TMDB_models;
using TMDB_demo.WebApi;

namespace Tests
{
    public class TmdbApiServiceTests : TmdbApiServiceTestBase
    {
        [Test]
        public async Task Result_Should_Be_TmdbMovie_Type()
        {
            SetUpHttpClientFactoryMock<TmdbMovie>();
            var tmdbApiService = new TmdbApiService(
                _httpClientFactoryMock.Object,
                Configuration);

            var result = await tmdbApiService.GetData<TmdbMovie>("movie/123");

            Assert.AreEqual(typeof(TmdbMovie), result.GetType());
        }

        [Test]
        public async Task Result_Should_Contains_Correct_Data() {
            SetUpHttpClientFactoryMock<TmdbMovie>();
            var tmdbApiService = new TmdbApiService(
                _httpClientFactoryMock.Object,
                Configuration);

            var result = await tmdbApiService.GetData<TmdbMovie>("movie/123");

            Assert.AreEqual(278, result.Id);
            Assert.AreEqual(142, result.Length);
            Assert.AreEqual("/q6y0Go1tsGEsmtFryDOJo3dEmqu.jpg", result.PosterUrl);
            Assert.AreEqual("The Shawshank Redemption", result.Title);
            Assert.AreEqual("Framed in the 1940s for the double murder of his wife and her lover, upstanding banker Andy Dufresne begins a new life at the Shawshank prison, where he puts his accounting skills to work for an amoral warden. During his long stretch in prison, Dufresne comes to be admired by the other inmates -- including an older prisoner named Red -- for his integrity and unquenchable sense of hope.", result.Overview);
            Assert.AreEqual(new DateTime(1994, 09, 23), result.ReleaseDate);
            Assert.AreEqual(8.7, result.VoteAverage);
            Assert.AreEqual(21145, result.VoteCount);
            Assert.That(result.Genres.Count() == 2);
            Assert.That(result.Genres.Any(x => x.Id == 18));
            Assert.That(result.Genres.Any(x => x.Id == 80));
        }
       
    }
}