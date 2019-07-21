using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.IO;

using TGUWeb.Models.WebApiModels;

namespace TGUWeb.Infrastructure
{
    public static class Database
    {
        public static ServiceUser GetServiceUser(int id)
        {
            return new ServiceUser(1, "Ben Emanuel", "5364758", false);
        }

        public static CarePlan GetCarePlan(int id)
        {
            CarePlan newCarePlan = new CarePlan(1);

            Challenge newChallenge = new Challenge(1, 1, "My Mental Health", "1", "2");
            Intervention intervention = new Intervention(1, "ABC", "DEF", true, false, "Client views\r\nare here.");
            intervention.Goals.Add(new Goal(1, 1, "Goal 1"));
            intervention.Goals.Add(new Goal(2, 1, "Goal 2"));
            intervention.Goals.Add(new Goal(3, 1, "Goal 3"));

            intervention.Activities.Add(new Activity(1, 1, "Activity 1"));
            intervention.Activities.Add(new Activity(2, 1, "Activity 2"));

            Intervention intervention2 = new Intervention(2, "GHI", "JKL", false, false, "Client views\r\nare here.");
            intervention2.Goals.Add(new Goal(4, 2, "Goal 4"));
            intervention2.Goals.Add(new Goal(5, 2, "Goal 5"));

            intervention2.Activities.Add(new Activity(3, 2, "Activity 3"));
            intervention2.Activities.Add(new Activity(4, 2, "Activity 4"));
            intervention2.Activities.Add(new Activity(5, 2, "Activity 5"));

            newChallenge.Interventions.Add(intervention);
            newChallenge.Interventions.Add(intervention2);

            Challenge newChallenge2 = new Challenge(2, 1, "Stopping My Problem Behaviours", "1", "2");
            Challenge newChallenge3 = new Challenge(3, 1, "Getting Insight", "1", "2");
            Challenge newChallenge4 = new Challenge(4, 1, "Recovery From Drug And Alcohol Problems", "1", "2");
            Challenge newChallenge5 = new Challenge(5, 1, "Making Feasable Plans", "1", "2");
            Challenge newChallenge6 = new Challenge(6, 1, "Staying Healthy", "1", "2");
            Challenge newChallenge7 = new Challenge(7, 1, "My Life Skills", "1", "2");
            Challenge newChallenge8 = new Challenge(8, 1, "My Relationships", "1", "2");

            newCarePlan.Challenges.Add(newChallenge);
            newCarePlan.Challenges.Add(newChallenge2);
            newCarePlan.Challenges.Add(newChallenge3);
            newCarePlan.Challenges.Add(newChallenge4);
            newCarePlan.Challenges.Add(newChallenge5);
            newCarePlan.Challenges.Add(newChallenge6);
            newCarePlan.Challenges.Add(newChallenge7);
            newCarePlan.Challenges.Add(newChallenge8);

            return newCarePlan;
        }

        public static CrisisPlan GetCrisisPlan(int patientId)
        {
            CrisisPlan plan = new CrisisPlan(patientId);

            plan.Sections.Add(new CrisisPlanSection(1, patientId, "If I start to feel unsafe or unwell I may... (What may happen if you go into crisis, does your sleep pattern change? Does your mood change?  What has happened in the past?", "My sleep is bad.\r\nI get angry all the time.\r\nI stop cleaning myself."));
            plan.Sections.Add(new CrisisPlanSection(2, patientId, "Things I need to do to keep myself safe and well...", "Keep taking my medication.\r\nExercise regularly.\r\nKeep appointments."));
            plan.Sections.Add(new CrisisPlanSection(3, patientId, "Things others need to do to keep me safe and well...", "Watch for my relapse indicators.\r\nTurn up to appointments."));

            return plan;
        }

        public static RelapsePreventionPlan GetRelapsePreventionPlan(int patientId)
        {
            RelapsePreventionPlan plan = new RelapsePreventionPlan(patientId);

            return plan;
        }

        public static Diary GetDiary(int patientId)
        {
            Diary diary = new Diary(1);

            diary.Appointments.Add(new Appointment(1, 1, 29, 6, 2019, "1pm", "Dr Kingham", "TGU"));
            diary.Appointments.Add(new Appointment(2, 1, 14, 7, 2019, "3pm", "Chris Collins", "TGU"));
            diary.Appointments.Add(new Appointment(3, 1, 18, 7, 2019, "9am", "Dr Kingham", "Home"));

            return diary;
        }

        public static List<PatientVideo> GetPatientVideos()
        {
            List<PatientVideo> videos = new List<PatientVideo>();

            string[] files = Directory.GetFiles("c:\\inetpub\\TGUApp\\PatientVideoFiles", "*.mp4");

            foreach (string file in files)
            {
                string shortName = file.Substring(file.LastIndexOf("\\") + 1, file.Length - file.LastIndexOf("\\") - 1);
                string dateOf = File.GetCreationTime(file).ToShortDateString();
                string timeOf = File.GetCreationTime(file).ToShortTimeString();

                videos.Add(new PatientVideo(shortName, dateOf, timeOf));
            }

            videos.Sort();

            return videos;
        }
    }
}