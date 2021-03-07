using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TourManager.Commands;
using TourManager.Models;

namespace TourManager.ViewModels
{
    public class AddTourViewModel
    {
        public AddTour Tour { get; }

        public bool CanUpdate
        {
            get
            {
                if (Tour == null)
                    return false;
                return !string.IsNullOrWhiteSpace(Tour.TitleName) && !string.IsNullOrWhiteSpace(Tour.Description) &&
                       !string.IsNullOrWhiteSpace(Tour.Source) && !string.IsNullOrWhiteSpace(Tour.Destination);

            }
        }

        public ICommand AddTourCommand
        {
            get;
        }

        public AddTourViewModel()
        {
            Tour = new AddTour();
            //Tour = new AddTour(Tour.TitleName, Tour.Source, Tour.Destination, Tour.Description, 4.3);
            AddTourCommand = new AddTourCommand(this);
        }


        public void SaveChanges()
        {

            //send to map updater.
            MessageBox.Show("Title: " + Tour.TitleName + "\nSource -> Destination: " + Tour.Source + " -> " +
                            Tour.Destination + "\nDescription: " + Tour.Description + "\n was added");

            //Debug.Assert(false, $"{Tour.TitleName + " " + Tour.Destination} was updated.");
        }
    }
}
