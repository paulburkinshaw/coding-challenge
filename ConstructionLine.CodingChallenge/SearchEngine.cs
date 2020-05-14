using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace ConstructionLine.CodingChallenge
{
    public class SearchEngine
    {
        private readonly List<Shirt> _shirts;

        public SearchEngine(List<Shirt> shirts)
        {
            _shirts = shirts;

            // TODO: data preparation and initialisation of additional data structures to improve performance goes here.

        }


        public SearchResults Search(SearchOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));
            if (options.Sizes == null)
                throw new ArgumentNullException(nameof(options.Sizes));
            if (options.Colors == null)
                throw new ArgumentNullException(nameof(options.Colors));


            var searchResults = new SearchResults()
            {
                Shirts = new List<Shirt>(),
                SizeCounts = (from size in Size.All select new SizeCount { Size = size }).ToList(),
                ColorCounts = (from colour in Color.All select new ColorCount { Color = colour }).ToList()
            };


            var filteredShirts = from shirt in _shirts
                                 where (options.Colors == null || !options.Colors.Any() || options.Colors.Any(c => c == shirt.Color)) &&
                                       (options.Sizes == null || !options.Sizes.Any() || options.Sizes.Any(s => s == shirt.Size))
                                 select shirt;

            var filteredShirtsWithCounts = (from shirt in filteredShirts
                                            group shirt by 1
                                            into shirtGroup
                                            select new
                                            {
                                                Shirts = from shirt in shirtGroup select shirt,
                                                SizeCounts = from size in Size.All
                                                             select new
                                                             {
                                                                 Id = size.Id,
                                                                 Count = shirtGroup.Count(s => s.Size.Id == size.Id)
                                                             },
                                                ColorCounts = from color in Color.All
                                                              select new
                                                              {
                                                                  Id = color.Id,
                                                                  Count = shirtGroup.Count(c => c.Color.Id == color.Id)
                                                              },
                                            }).FirstOrDefault();


            if (filteredShirtsWithCounts == null)
                return searchResults;


            searchResults.Shirts = filteredShirtsWithCounts.Shirts.ToList();

            foreach (var sizeCount in filteredShirtsWithCounts.SizeCounts.Where(sc => sc.Count > 0))
            {
                searchResults.SizeCounts.FirstOrDefault(srsc => srsc.Size.Id == sizeCount.Id).Count = sizeCount.Count;
            }

            foreach (var colorCount in filteredShirtsWithCounts.ColorCounts.Where(cc => cc.Count > 0))
            {
                searchResults.ColorCounts.FirstOrDefault(srcc => srcc.Color.Id == colorCount.Id).Count = colorCount.Count;
            }

            return searchResults;
        }
    }
}