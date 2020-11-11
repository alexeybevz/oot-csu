using System.Collections.Generic;
using Xunit;

namespace Homework02
{
    public class UnitTests
    {
        IBookItemVisitor bookItemVisitor = new BaseBookItemVisitor();

        [Fact]
        public void OrderNotFreeDeliveryTest()
        {
            var b1 = new PaperBook() {Title = "b1", Price = 200};
            var bi1 = new PaperBookItem() {Book = b1, Qty = 4};
            var b2 = new PaperBook() { Title = "b2", Price = 100 };
            var bi2 = new PaperBookItem() { Book = b2, Qty = 1 };
            var b3 = new ElectronicBook() { Title = "b3", Price = 400 };
            var bi3 = new ElectronicBookItem() { Book = b3, Qty = 1 };

            var cart = new ShoppingCart(bookItemVisitor);
            cart.Add(bi1);
            cart.Add(bi2);
            cart.Add(bi3);
            Assert.Equal(1500, cart.GetTotal());
        }

        [Fact]
        public void OrderFreeDeliveryTest()
        {
            var b1 = new PaperBook() { Title = "b1", Price = 200 };
            var bi1 = new PaperBookItem() { Book = b1, Qty = 7 };
            var b2 = new PaperBook() { Title = "b2", Price = 100 };
            var bi2 = new PaperBookItem() { Book = b2, Qty = 1 };
            var b3 = new ElectronicBook() { Title = "b3", Price = 400 };
            var bi3 = new ElectronicBookItem() { Book = b3, Qty = 1 };

            var cart = new ShoppingCart(bookItemVisitor);
            cart.Add(bi1);
            cart.Add(bi2);
            cart.Add(bi3);
            Assert.Equal(1900, cart.GetTotal());
        }

        [Fact]
        public void OrderFreeDeliveryAndDiscountsTest()
        {
            var b1 = new PaperBook() { Title = "b1", Price = 200 };
            var bi1 = new PaperBookItem() { Book = b1, Qty = 7 };
            var b2 = new PaperBook() { Title = "b2", Price = 100 };
            var bi2 = new PaperBookItem() { Book = b2, Qty = 1 };
            var b3 = new ElectronicBook() { Title = "b3", Price = 400 };
            var bi3 = new ElectronicBookItem() { Book = b3, Qty = 1 };

            var promos = new List<IPromo>()
            {
                new DiscountPercent(10, CostPromoPriority.Medium),
                new DiscountCurrency(200, CostPromoPriority.High),
            };

            var cart = new ShoppingCart(bookItemVisitor, promos);
            cart.Add(bi1);
            cart.Add(bi2);
            cart.Add(bi3);
            Assert.Equal(1530, cart.GetTotal());
        }

        [Fact]
        public void OrderFreeDeliveryAndDiscountsAndOtherPromoTest()
        {
            var b1 = new PaperBook() { Author = "a1", Title = "b1", Price = 200 };
            var bi1 = new PaperBookItem() { Book = b1, Qty = 3 };
            var b2 = new PaperBook() { Author = "a2", Title = "b2", Price = 100 };
            var bi2 = new PaperBookItem() { Book = b2, Qty = 1 };
            var b3 = new ElectronicBook() { Author = "a3", Title = "b3", Price = 400 };
            var bi3 = new ElectronicBookItem() { Book = b3, Qty = 1 };

            var promos = new List<IPromo>()
            {
                new DiscountPercent(10, CostPromoPriority.Medium),
                new DiscountCurrency(200, CostPromoPriority.High),
                new FreeBookPromo(new PaperBook() { Author = "a1", Title = "b1"}, CostPromoPriority.High),
                new FreeBookPromo(new ElectronicBook() { Author = "a3", Title = "b3"}, CostPromoPriority.High),
                new FreeDeliveryPromo(CostPromoPriority.Medium),
            };

            var cart = new ShoppingCart(bookItemVisitor, promos);
            cart.Add(bi1);
            cart.Add(bi2);
            cart.Add(bi3);
            Assert.Equal(270, cart.GetTotal());
        }
    }
}