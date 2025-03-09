using Moq;
using RP1_L00148202.Pages.Customer.Home;
using RP1.Models;
using RP1.Services;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using RP1.DataAccess.Repository;

public class DetailsModelTests
{
    [Fact]
    public void OnPost_ValidModel_AddsShoppingCart()
    {
        // Arrange
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var mockShoppingCartRepo = new Mock<IShoppingCartRepo>();
        mockUnitOfWork.Setup(u => u.ShoppingCartRepo).Returns(mockShoppingCartRepo.Object);

        var detailsModel = new DetailsModel(mockUnitOfWork.Object)
        {
            ShoppingCart = new ShoppingCart
            {
                ApplicationUserId = "test-user",
                ProductId = 1,
                Quantity = 1
            }
        };

        // Act
        var result = detailsModel.OnPost();

        // Assert
        mockShoppingCartRepo.Verify(s => s.Add(It.IsAny<ShoppingCart>()), Times.Once);
        mockUnitOfWork.Verify(u => u.SaveAsync(), Times.Once);
        Assert.IsType<RedirectToPageResult>(result);
    }
}
