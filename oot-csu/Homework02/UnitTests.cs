using System.Collections.Generic;
using BusinessLogic;
using BusinessLogic.Promo;
using Moq;
using Xunit;

namespace Homework02
{
    public class UnitTests
    {
        [Fact]
        public void OrderNotFreeDeliveryTest()
        {
            var b1 = new PaperBook() { Title = "b1", Price = 200, Qty = 4 };
            var b2 = new PaperBook() { Title = "b2", Price = 100 };
            var b3 = new DigitalBook() { Title = "b3", Price = 400 };

            var cart = new ShoppingCart(new CartTotalsCalculator());
            cart.Add(b1);
            cart.Add(b2);
            cart.Add(b3);
            Assert.Equal(1500, cart.GetTotal());
        }

        [Fact]
        public void OrderFreeDeliveryTest()
        {
            var b1 = new PaperBook() { Title = "b1", Price = 200, Qty = 7 };
            var b2 = new PaperBook() { Title = "b2", Price = 100 };
            var b3 = new DigitalBook() { Title = "b3", Price = 400 };

            var cart = new ShoppingCart(new CartTotalsCalculator());
            cart.Add(b1);
            cart.Add(b2);
            cart.Add(b3);
            Assert.Equal(1900, cart.GetTotal());
        }

        [Fact]
        public void OrderFreeDeliveryAndDiscountsTest()
        {
            var b1 = new PaperBook() { Title = "b1", Price = 200, Qty = 7 };
            var b2 = new PaperBook() { Title = "b2", Price = 100 };
            var b3 = new DigitalBook() { Title = "b3", Price = 400 };

            var promos = new List<IPromo>()
            {
                new DiscountPercent(10, CostPromoPriority.Medium),
                new DiscountCurrency(200, CostPromoPriority.High),
            };

            var cart = new ShoppingCart(new CartTotalsCalculator(promos));
            cart.Add(b1);
            cart.Add(b2);
            cart.Add(b3);
            Assert.Equal(1530, cart.GetTotal());
        }

        [Fact]
        public void OrderFreeDeliveryAndDiscountsAndOtherPromoTest()
        {
            var b1 = new PaperBook() { Author = "a1", Title = "b1", Price = 200, Qty = 3 };
            var b2 = new PaperBook() { Author = "a2", Title = "b2", Price = 100 };
            var b3 = new DigitalBook() { Author = "a3", Title = "b3", Price = 400 };

            var promos = new List<IPromo>()
            {
                new DiscountPercent(10, CostPromoPriority.Medium),
                new DiscountCurrency(200, CostPromoPriority.High),
                new FreeBookPromo(new PaperBook() { Author = "a1", Title = "b1"}, CostPromoPriority.High),
                new FreeBookPromo(new DigitalBook() { Author = "a3", Title = "b3"}, CostPromoPriority.High),
                new FreeDeliveryPromo(CostPromoPriority.Medium),
            };

            var cart = new ShoppingCart(new CartTotalsCalculator(promos));
            cart.Add(b1);
            cart.Add(b2);
            cart.Add(b3);
            Assert.Equal(270, cart.GetTotal());
        }

        [Fact]
        public void AddFreeDigitalBookOnEachSecondPaperBookTest()
        {
            var bookRepository = new Mock<IBookRepository>();

            bookRepository
                .Setup(x => x.GetBooks())
                .Returns(new List<Book>()
                {
                    new PaperBook() { Author = "a2", Title = "b4", Price = 300 },
                    new PaperBook() { Author = "a2", Title = "b5", Price = 400 },
                    new PaperBook() { Author = "a2", Title = "b6", Price = 500 },
                    new DigitalBook() { Author = "a2", Title = "d1", Price = 500 },
                    new DigitalBook() { Author = "a2", Title = "d2", Price = 300 },
                });

            List<Book> orderedBooks = new List<Book>()
            {
                new PaperBook() { Author = "a1", Title = "bb1", Price = 100, Qty = 1 },
                new PaperBook() { Author = "a1", Title = "bb2", Price = 100, Qty = 1 },
                new PaperBook() { Author = "a2", Title = "b1", Price = 200, Qty = 1 },
                new PaperBook() { Author = "a2", Title = "b2", Price = 200, Qty = 1 },
                new PaperBook() { Author = "a2", Title = "b3", Price = 300, Qty = 1 },
            };

            var promos = new List<IPromo>()
            {
                new ExtraFreeBooksPromo(bookRepository.Object),
            };

            var cart = new ShoppingCart(new CartTotalsCalculator(promos));
            cart.AddRange(orderedBooks);
            Assert.Equal(orderedBooks.Count + 1, cart.GetOrderedBooks().Count + cart.GetExtraFreeBooks().Count);
        }
    }
}