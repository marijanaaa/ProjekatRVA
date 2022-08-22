///////////////////////////////////////////////////////////
//  Planner.cs
//  Implementation of the Class Planner
//  Generated by Enterprise Architect
//  Created on:      25-Jul-2022 7:42:22 PM
//  Original author: zeljko.cavic
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;



namespace ProjekatRVA.Models
{
	public class Planner {

		public User user;
		public int userId;
		public List<Event> events;
		private int id;
		private string plannerName;
		public int Id { get { return id; } set { id = value; } }
		public string PlannerName { get { return plannerName; } set { plannerName = value; } }
		public DateTime Time { get; set; }

		public Planner(){

		}

		~Planner(){

		}

	}//end Planner

}//end namespace EventPlanner