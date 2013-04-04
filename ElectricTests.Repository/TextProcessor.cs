using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ElectricTests.Model;

namespace ElectricTests.Repository
{

	public class TextProcessor 
	{
		//Document text from the form 
		public string InputText { get; set; }

		/// <summary>
		/// Split text to row strings
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public string[] GetArrayFromText(string text) 
		{
			string[] parts = text.Trim().Split('\n');
			return parts;
		}	
	}

	public class STProcessor : TextProcessor 
	{

		public string PreviousParagraphText { get; set; }
		private Paragraph PreviousParagraph { get; set; }
		private Paragraph CurrentParagraph { get; set; }

		/// <summary>
		/// Get paragraphs set from sting array 
		/// </summary>
		/// <param name="paragraphs"></param>
		/// <returns></returns>
		public HashSet<Paragraph> GetParagraphsFromArray (string[] paragraphs) 
		{
			var paragraphsList = new HashSet<Paragraph>();
			foreach (string paragraphString in paragraphs) 
			{
				//If paragraph having a paragraph number ...
				if (splitStringToParagraph(paragraphString)) 
				{
					//...and we have previous paragraph then we must save old one to the list
					if (PreviousParagraph != null) {
						paragraphsList.Add(PreviousParagraph);

						if(CurrentParagraph.ParentNumber != "0") {
							foreach (var paragraph in paragraphsList) {
								string paragraphNumber = paragraph.Number.ToString();
								string fullParagraphNumber = (paragraph.ParentNumber != "0")
									                             ? paragraph.ParentNumber + "." + paragraphNumber
									                             : paragraphNumber;
								
								if (CurrentParagraph.ParentNumber == fullParagraphNumber)
									paragraph.Paragraphs.Add(CurrentParagraph);
							}
						}						
					}
					PreviousParagraph = CurrentParagraph;
				}
				// if don't have an number, it's a previous paragraph text
				else if (PreviousParagraph != null)
					PreviousParagraph.Text += "\n" + PreviousParagraphText;
			}
			if (PreviousParagraph != null)
				paragraphsList.Add(PreviousParagraph);
			return paragraphsList;
		}

		/// <summary>
		/// Split paragraph string to Paragraph object or to previous paragraph text
		/// </summary>
		/// <param name="paragraphString"></param>
		/// <returns></returns>
		public bool splitStringToParagraph(string paragraphString) {
			
			
			Match match = MatchToParagraph(paragraphString);

			//If matches we have new paragraph with index, else this is previous paragraph text
			if (match.Success) 
			{
				// Like 1.5.6.6.8. Paragraph text.
				const int maxParagraphDeep = 5;

				//List for saving and format parent paragraph number
				List<string> paragraphNumbers = new List<string>();
				for (var i = 1; i < maxParagraphDeep; i++ ) {
					if(match.Groups[i].Success) {
						paragraphNumbers.Add(match.Groups[i].ToString());
					}
				}
				string paragraphText = match.Groups[6].ToString();
				int paragraphNumber = Int32.Parse(paragraphNumbers[paragraphNumbers.Count - 1]);
				// 0 for paragraphs without parents
				string parentParagraphNumber = "0";
				
				//If we have many paragraph numbers, then we have parent paragraph
				if(paragraphNumbers.Count > 1) {
					paragraphNumbers.RemoveAt(paragraphNumbers.Count - 1);
					parentParagraphNumber = String.Join(".", paragraphNumbers.ToArray());
				}

				CurrentParagraph = new Paragraph(paragraphText, paragraphNumber, parentParagraphNumber);
				PreviousParagraphText = "";
				return true;
			}
			PreviousParagraphText = paragraphString;
			return false;
		}

		/// <summary>
		/// Match string to paragraph template with 5 deep level
		/// </summary>
		/// <param name="paragraphString"></param>
		/// <returns></returns>
		public Match MatchToParagraph(string paragraphString) {
			var regex = new Regex(@"(\d+)\.(?:(\d+)\.)?(?:(\d+)\.)?(?:(\d+)\.)?(?:(\d+)\.)?\s(.+)");
			return regex.Match(paragraphString);
		}

		/// <summary>
		/// Get paragraph set from text
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public HashSet<Paragraph> GetParagraphsFromText (string text) 
		{
			return GetParagraphsFromArray(GetArrayFromText(text));
		}
	}
}