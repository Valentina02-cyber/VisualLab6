using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Planner.Models;

namespace Planner.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        string description = "";
        PlannerList current = null;
        bool isEditing = false;

        string heading = "";
        public string Heading
        {
            get => heading;
            set => this.RaiseAndSetIfChanged(ref heading, value);
        }
        public string Description
        {
            get => description;
            set => this.RaiseAndSetIfChanged(ref description, value);
        }

        DateTimeOffset date = DateTimeOffset.Now.Date;
        public DateTimeOffset Date
        {
            set
            {
                this.RaiseAndSetIfChanged(ref date, value);
                this.ChangeObservableCollection(date);
            }
            get => date;
        }
        public ObservableCollection<PlannerList> Items { get; set; }


        ViewModelBase content;
        public ViewModelBase Content
        {
            get => content;
            private set => this.RaiseAndSetIfChanged(ref content, value);
        }

        public MainWindowViewModel()
        {
            ListByDays = new Dictionary<DateTimeOffset, List<PlannerList>>();
            Items = new ObservableCollection<PlannerList>();
            Content = new PlannerListViewModel();
        }

        private Dictionary<DateTimeOffset, List<PlannerList>> ListByDays;
        private void InitToDoList()
        {
            ListByDays = new Dictionary<DateTimeOffset, List<PlannerList>>();
            ListByDays.Add(date, new List<PlannerList>());
        }

        public void AppendAction(DateTimeOffset date, PlannerList item)
        {
            if (!ListByDays.ContainsKey(date))
                ListByDays.Add(date, new List<PlannerList>());
            ListByDays[date].Add(item);
            this.ChangeObservableCollection(Date);
        }


        public void ChangeView()
        {
            if (this.Content is PlannerListViewModel)
                this.Content = new NoteViewModel();
            else
            {
                Heading = "";
                Description = "";
                current = null;
                isEditing = false;
                Content = new PlannerListViewModel();
            }
        }

        public void ChangeObservableCollection(DateTimeOffset date)
        {
            if (!ListByDays.ContainsKey(date))
                Items.Clear();
            else
            {
                Items.Clear();
                foreach (var item in ListByDays[date])
                    Items.Add(item);
            }
        }

        public void Save()
        {
            if (Heading != "")
            {
                if (isEditing)
                {
                    var item = ListByDays[date].Find(x => x.Equals(current));
                    item.Heading = this.Heading;
                    item.Description = this.Description;
                    isEditing = false;
                }
                else
                    AppendAction(Date, new PlannerList(Heading, Description));
                ChangeView();
            }
        }

        public void Delete(PlannerList item)
        {
            ListByDays[date].Remove(item);
            ChangeObservableCollection(date);
        }

        public void ViewExisting(PlannerList item)
        {
            isEditing = true;
            current = item;
            Heading = current.Heading;
            Description = current.Description;
            ChangeView();
        }
    }
}
