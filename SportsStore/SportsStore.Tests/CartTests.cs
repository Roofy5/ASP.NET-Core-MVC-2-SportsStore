using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SportsStore.Tests
{
    public class CartTests
    {
        [Fact]
        public void Can_Add_New_Lines()
        {
            //arrange
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };
            Cart sut = new Cart();

            //act
            sut.AddItem(p1, 1);
            sut.AddItem(p2, 1);
            CartLine[] result = sut.Lines.ToArray();

            //assert
            Assert.Equal(2, result.Length);
            Assert.Equal(p1, result[0].Product);
            Assert.Equal(p2, result[1].Product);
        }
        [Fact]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            //arrange
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };
            Cart sut = new Cart();

            //act
            sut.AddItem(p1, 1);
            sut.AddItem(p2, 1);
            sut.AddItem(p1, 10);
            CartLine[] result = sut.Lines
                .OrderBy(l => l.Product.ProductID)
                .ToArray();

            //assert
            Assert.Equal(2, result.Length);
            Assert.Equal(11, result[0].Quantity);
            Assert.Equal(1, result[1].Quantity);
        }
        [Fact]
        public void Can_Remove_Line()
        {
            //arrange
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };
            Product p3 = new Product { ProductID = 3, Name = "P3" };
            Cart sut = new Cart();

            //act
            sut.AddItem(p1, 1);
            sut.AddItem(p2, 3);
            sut.AddItem(p3, 5);
            sut.AddItem(p2, 1);
            sut.RemoveLine(p2);

            //assert
            Assert.Empty(sut.Lines.Where(l => l.Product == p2));
            Assert.Equal(2, sut.Lines.Count());
        }
        [Fact]
        public void Calculate_Cart_Total()
        {
            //arrange
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 100M };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 50M };
            Cart sut = new Cart();

            //act
            sut.AddItem(p1, 1);
            sut.AddItem(p2, 1);
            sut.AddItem(p1, 3);
            decimal result = sut.ComputeTotalValue();

            //assert
            Assert.Equal(450M, result);
        }
        [Fact]
        public void Can_Clear_Contents()
        {
            //arrange
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 100M };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 50M };
            Cart sut = new Cart();

            //act
            sut.AddItem(p1, 1);
            sut.AddItem(p2, 1);
            sut.Clear();

            //assert
            Assert.Empty(sut.Lines);
        }
    }
}
