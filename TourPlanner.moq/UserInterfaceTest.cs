using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Extras.Moq;
using NUnit.Framework;
using TourPlanner.BL.Database.Log;
using TourPlanner.DAL.Log;
using TourPlanner.ViewModels.Tour;

namespace TourPlanner.UnitTests
{
    public class UserInterfaceTest
    {
        [Test]
        public void AddButtonEnabled_AddButtonIsEnabled()
        {
            var tourViewModel = new TourViewModel
            {
                Name = "testName",
                Description = "testDesc",
                Source = "testSrc",
                Destination = "testDest",
                Distance = 1
            };

            Assert.IsTrue(tourViewModel.CanUpdate);
        }

        [Test]
        public void AddButtonDisabled_AddButtonIsDisabled()
        {
            var tourViewModel = new TourViewModel
            {
                Name = "testName",
                Description = "testDesc",
                Source = "testSrc",
                Destination = "testDest",
                Distance = 0
            };

            Assert.IsFalse(tourViewModel.CanUpdate);
        }

        [Test]
        public void ValidateButtonDisabled_ValidateButtonIsDisabled()
        {
            var tourViewModel = new TourViewModel
            {
                Name = "testName", 
                Destination = "testDest",
                Distance = 0
            };

            Assert.IsFalse(tourViewModel.CanUpdateValidate);
        }

        [Test]
        public void ValidateButtonEnabled_ValidateButtonIsEnabled()
        {
            var tourViewModel = new TourViewModel
            {
                Name = "testName",
                Description = "testDesc",
                Source = "testSrc",
                Destination = "testDest",
                Distance = 0
            };

            Assert.IsTrue(tourViewModel.CanUpdateValidate);
        }

    }
}
