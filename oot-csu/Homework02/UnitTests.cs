using System.Collections.Generic;
using Xunit;

namespace Homework02
{
    public class UnitTests
    {
        PaperBookVisitor paperBookDelivery = new PaperBookVisitor();
        ElectronicBookVisitor electronicBookDelivery = new ElectronicBookVisitor();

        [Fact]
        public void OrderNotFreeDeliveryTest()
        {
            var b1 = new PaperBook() {Title = "b1", Price = 200};
            var bi1 = new PaperBookItem(paperBookDelivery) {Book = b1, Qty = 4};
            var b2 = new PaperBook() { Title = "b2", Price = 100 };
            var bi2 = new PaperBookItem(paperBookDelivery) { Book = b2, Qty = 1 };
            var b3 = new ElectronicBook() { Title = "b3", Price = 400 };
            var bi3 = new ElectronicBookItem(electronicBookDelivery) { Book = b3, Qty = 1 };

            var bookVisitors = new List<BookVisitor> {paperBookDelivery, electronicBookDelivery};

            var cart = new ShoppingCart(bookVisitors);
            cart.Add(bi1);
            cart.Add(bi2);
            cart.Add(bi3);
            Assert.Equal(1500, cart.GetTotal());
        }

        [Fact]
        public void OrderFreeDeliveryTest()
        {
            var b1 = new PaperBook() { Title = "b1", Price = 200 };
            var bi1 = new PaperBookItem(paperBookDelivery) { Book = b1, Qty = 7 };
            var b2 = new PaperBook() { Title = "b2", Price = 100 };
            var bi2 = new PaperBookItem(paperBookDelivery) { Book = b2, Qty = 1 };
            var b3 = new ElectronicBook() { Title = "b3", Price = 400 };
            var bi3 = new ElectronicBookItem(electronicBookDelivery) { Book = b3, Qty = 1 };

            var bookVisitors = new List<BookVisitor> {paperBookDelivery, electronicBookDelivery};

            var cart = new ShoppingCart(bookVisitors);
            cart.Add(bi1);
            cart.Add(bi2);
            cart.Add(bi3);
            Assert.Equal(1900, cart.GetTotal());
        }

        [Fact]
        public void OrderFreeDeliveryAndDiscountsTest()
        {
            var b1 = new PaperBook() { Title = "b1", Price = 200 };
            var bi1 = new PaperBookItem(paperBookDelivery) { Book = b1, Qty = 7 };
            var b2 = new PaperBook() { Title = "b2", Price = 100 };
            var bi2 = new PaperBookItem(paperBookDelivery) { Book = b2, Qty = 1 };
            var b3 = new ElectronicBook() { Title = "b3", Price = 400 };
            var bi3 = new ElectronicBookItem(electronicBookDelivery) { Book = b3, Qty = 1 };

            var bookVisitors = new List<BookVisitor> { paperBookDelivery, electronicBookDelivery };
            var costPromos = new List<ICostPromo>()
            {
                new DiscountPercent(10, CostPromoPriority.Medium),
                new DiscountCurrency(200, CostPromoPriority.High),
            };

            var cart = new ShoppingCart(bookVisitors, costPromos);
            cart.Add(bi1);
            cart.Add(bi2);
            cart.Add(bi3);
            Assert.Equal(1530, cart.GetTotal());
        }

        [Fact]
        public void OrderFreeDeliveryAndDiscountsAndOtherPromoTest()
        {
            var b1 = new PaperBook() { Author = "a1", Title = "b1", Price = 200 };
            var bi1 = new PaperBookItem(paperBookDelivery) { Book = b1, Qty = 3 };
            var b2 = new PaperBook() { Author = "a2", Title = "b2", Price = 100 };
            var bi2 = new PaperBookItem(paperBookDelivery) { Book = b2, Qty = 1 };
            var b3 = new ElectronicBook() { Author = "a3", Title = "b3", Price = 400 };
            var bi3 = new ElectronicBookItem(electronicBookDelivery) { Book = b3, Qty = 1 };

            var bookVisitors = new List<BookVisitor> { paperBookDelivery, electronicBookDelivery };
            var costPromos = new List<ICostPromo>()
            {
                new DiscountPercent(10, CostPromoPriority.Medium),
                new DiscountCurrency(200, CostPromoPriority.High),
            };
            var freeBookPromos = new List<IBookPromo>()
            {
                new FreeBookPromo(new Book() { Author = "a1", Title = "b1"}),
                new FreeBookPromo(new Book() { Author = "a3", Title = "b3" })
            };
            var freeDeliveryPromo = new FreeDeliveryPromo();

            var cart = new ShoppingCart(bookVisitors, costPromos, freeBookPromos, freeDeliveryPromo);
            cart.Add(bi1);
            cart.Add(bi2);
            cart.Add(bi3);
            Assert.Equal(270, cart.GetTotal());
        }
    }
}