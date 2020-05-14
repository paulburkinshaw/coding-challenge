using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace ConstructionLine.CodingChallenge.Tests
{
    [TestFixture]
    public class SearchEngineTests : SearchEngineTestsBase
    {

        [TestFixture]
        public class SearchTests
        {

            #region invalid inputs

            [Test]
            public void When_searchOptions_is_null_Should_throw_ArgumentNullException()
            {
                var shirts = new List<Shirt>
                {
                    new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red)
                };

                var searchEngine = new SearchEngine(shirts);

                SearchOptions searchOptions = null;

                Assert.Throws<ArgumentNullException>(() =>
                {
                    searchEngine.Search(searchOptions);
                });
            }

            [Test]
            public void When_searchOptionsColors_is_null_Should_throw_ArgumentNullException()
            {
                var shirts = new List<Shirt>
                {
                    new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red)
                };

                var searchEngine = new SearchEngine(shirts);

                var searchOptions = new SearchOptions
                {
                    Colors = null,
                    Sizes = new List<Size> { Size.Small }
                };

                Assert.Throws<ArgumentNullException>(() =>
                {
                    searchEngine.Search(searchOptions);
                });
            }

            [Test]
            public void When_searchOptionsSizes_is_null_Should_throw_ArgumentNullException()
            {
                var shirts = new List<Shirt>
                {
                    new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red)
                };

                var searchEngine = new SearchEngine(shirts);

                var searchOptions = new SearchOptions
                {
                    Colors = new List<Color> { Color.Red },
                    Sizes = null
                };

                Assert.Throws<ArgumentNullException>(() =>
                {
                    searchEngine.Search(searchOptions);
                });
            }


            #endregion

            #region valid inputs

            [Test]
            public void When_one_colour_and_one_size_specified_And_shirts_available_Should_return_matching_shirt()
            {
                var shirts = new List<Shirt>
                {
                    new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                    new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                    new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
                };

                var searchEngine = new SearchEngine(shirts);

                var searchOptions = new SearchOptions
                {
                    Colors = new List<Color> { Color.Red },
                    Sizes = new List<Size> { Size.Small }
                };

                var results = searchEngine.Search(searchOptions);

                AssertResults(results.Shirts, searchOptions);
                AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
                AssertColorCounts(shirts, searchOptions, results.ColorCounts);
            }

            [Test]
            public void When_multiple_colours_and_one_size_specified_And_shirts_available_Should_return_matching_shirt()
            {
                var shirts = new List<Shirt>
                {
                    new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                    new Shirt(Guid.NewGuid(), "Blue - Small", Size.Small, Color.Blue),
                    new Shirt(Guid.NewGuid(), "Yellow - Small", Size.Small, Color.Yellow),
                    new Shirt(Guid.NewGuid(), "White - Small", Size.Small, Color.White),
                    new Shirt(Guid.NewGuid(), "Black - Small", Size.Small, Color.Black),
                    new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                    new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
                };

                var searchEngine = new SearchEngine(shirts);

                var searchOptions = new SearchOptions
                {
                    Colors = new List<Color> { Color.Red, Color.Black },
                    Sizes = new List<Size> { Size.Small }
                };

                var results = searchEngine.Search(searchOptions);

                AssertResults(results.Shirts, searchOptions);
                AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
                AssertColorCounts(shirts, searchOptions, results.ColorCounts);
            }

            [Test]
            public void When_multiple_sizes_and_one_color_specified_And_shirts_available_Should_return_matching_shirt()
            {
                var shirts = new List<Shirt>
                {
                    new Shirt(Guid.NewGuid(), "Black - Small", Size.Small, Color.Black),
                    new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                    new Shirt(Guid.NewGuid(), "Black - Large", Size.Large, Color.Black),
                    new Shirt(Guid.NewGuid(), "Blue - Small", Size.Small, Color.Blue),
                    new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                    new Shirt(Guid.NewGuid(), "Yellow - Medium", Size.Medium, Color.Yellow),
                };

                var searchEngine = new SearchEngine(shirts);

                var searchOptions = new SearchOptions
                {
                    Colors = new List<Color> { Color.Black },
                    Sizes = new List<Size> { Size.Small, Size.Medium, Size.Large }
                };

                var results = searchEngine.Search(searchOptions);

                AssertResults(results.Shirts, searchOptions);
                AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
                AssertColorCounts(shirts, searchOptions, results.ColorCounts);
            }

            [Test]
            public void When_one_colour_but_no_sizes_specified_And_shirts_available_Should_return_matching_shirt()
            {
                var shirts = new List<Shirt>
                {
                    new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red)
                };

                var searchEngine = new SearchEngine(shirts);

                var searchOptions = new SearchOptions
                {
                    Colors = new List<Color> { Color.Red }
                };

                var results = searchEngine.Search(searchOptions);

                AssertResults(results.Shirts, searchOptions);
                AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
                AssertColorCounts(shirts, searchOptions, results.ColorCounts);
            }

            [Test]
            public void When_multiple_colors_but_no_sizes_specified_And_shirts_available_Should_return_matching_shirt()
            {
                var shirts = new List<Shirt>
                {
                    new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                    new Shirt(Guid.NewGuid(), "Blue - Small", Size.Small, Color.Blue),
                    new Shirt(Guid.NewGuid(), "Yellow - Small", Size.Small, Color.Yellow),
                    new Shirt(Guid.NewGuid(), "White - Small", Size.Small, Color.White),
                    new Shirt(Guid.NewGuid(), "Black - Small", Size.Small, Color.Black),
                    new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                    new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
                };

                var searchEngine = new SearchEngine(shirts);

                var searchOptions = new SearchOptions
                {
                    Colors = new List<Color> { Color.Red, Color.Yellow, Color.White }
                };

                var results = searchEngine.Search(searchOptions);

                AssertResults(results.Shirts, searchOptions);
                AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
                AssertColorCounts(shirts, searchOptions, results.ColorCounts);
            }

            [Test]
            public void When_one_size_but_no_colour_specified_And_shirts_available_Should_return_matching_shirt()
            {
                var shirts = new List<Shirt>
                {
                    new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red)
                };

                var searchEngine = new SearchEngine(shirts);

                var searchOptions = new SearchOptions
                {
                    Sizes = new List<Size> { Size.Small }
                };

                var results = searchEngine.Search(searchOptions);

                AssertResults(results.Shirts, searchOptions);
                AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
                AssertColorCounts(shirts, searchOptions, results.ColorCounts);
            }

            [Test]
            public void When_multiple_sizes_but_no_colors_specified_And_shirts_available_Should_return_matching_shirt()
            {
                var shirts = new List<Shirt>
                {
                    new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                    new Shirt(Guid.NewGuid(), "Blue - Medium", Size.Medium, Color.Blue),
                    new Shirt(Guid.NewGuid(), "Yellow - Large", Size.Large, Color.Yellow),
                    new Shirt(Guid.NewGuid(), "White - Small", Size.Small, Color.White),
                    new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                    new Shirt(Guid.NewGuid(), "Black - Large", Size.Large, Color.Black),
                    new Shirt(Guid.NewGuid(), "Blue - Small", Size.Small, Color.Blue),
                };

                var searchEngine = new SearchEngine(shirts);

                var searchOptions = new SearchOptions
                {
                    Sizes = new List<Size> { Size.Small, Size.Medium, Size.Large }
                };

                var results = searchEngine.Search(searchOptions);

                AssertResults(results.Shirts, searchOptions);
                AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
                AssertColorCounts(shirts, searchOptions, results.ColorCounts);
            }

            [Test]
            public void When_one_colour_and_one_size_specified_And_shirt_not_available_in_specified_colour_Should_not_return_shirts()
            {
                var shirts = new List<Shirt>
                {
                    new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Black),    
                };

                var searchEngine = new SearchEngine(shirts);

                var searchOptions = new SearchOptions
                {
                    Colors = new List<Color> { Color.Red },
                    Sizes = new List<Size> { Size.Small }
                };

                var results = searchEngine.Search(searchOptions);

                AssertResults(results.Shirts, searchOptions);
                AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
                AssertColorCounts(shirts, searchOptions, results.ColorCounts);
            }

            [Test]
            public void When_one_colour_and_one_size_specified_And_no_matching_shirts_in_specified_size_Should_not_return_shirts()
            {
                var shirts = new List<Shirt>
                {
                    new Shirt(Guid.NewGuid(), "Red - Small", Size.Medium, Color.Red),
                };

                var searchEngine = new SearchEngine(shirts);

                var searchOptions = new SearchOptions
                {
                    Colors = new List<Color> { Color.Red },
                    Sizes = new List<Size> { Size.Small }
                };

                var results = searchEngine.Search(searchOptions);

                AssertResults(results.Shirts, searchOptions);
                AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
                AssertColorCounts(shirts, searchOptions, results.ColorCounts);
            }

            [Test]
            public void When_no_colours_or_sizes_specified_And_shirts_available_Should_return_all_available_shirts()
            {
                var shirts = new List<Shirt>
                {
                    new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                    new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                    new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
                    new Shirt(Guid.NewGuid(), "Black - Large", Size.Large, Color.Black),
                    new Shirt(Guid.NewGuid(), "Yellow - Small", Size.Small, Color.Yellow),
                    new Shirt(Guid.NewGuid(), "White - Small", Size.Small, Color.White)
                };

                var searchEngine = new SearchEngine(shirts);

                var searchOptions = new SearchOptions();

                var results = searchEngine.Search(searchOptions);

                AssertResults(results.Shirts, searchOptions);
                AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
                AssertColorCounts(shirts, searchOptions, results.ColorCounts);
            }

            #endregion
        }


    }
}
