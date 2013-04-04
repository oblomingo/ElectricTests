using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ElectricTests.Repository;
using ElectricTests.Model;

namespace ElectricTests.Tests
{
	[TestClass]
	public class TextProcessorTest {
		private const string text = "1.	Pirmas.\n" + // 0 group paragraph - 0 string
		                    "1.1.	Pirmas-pirmas.\n" + // 1 group paragraph - 1 string
		                    "1.2.	Pirmas-antras.\n" + // 2 group paragraph - 2 string
		                    "Pirmas-antras continue\n" + // 3 string
		                    "1.2.1.	Pirmas-antras-pirmas.\n" + // 3 group paragraph - 4 string
		                    "1.2.1.4.	Pirmas-antras-pirmas-ketvirtas.\n" + // 4 group paragraph - 5 string
		                    "Pirmas-antras-pirmas-ketvirtas continue.\n" + // 6 string
		                    "1.2.2.	Pirmas-antras-antras.\n" + // 5 group paragraph - 7 string
		                    "1.2.2.1.	Pirmas-antras-antras-pirmas.\n" + // 6 group paragraph - 8 string
		                    "1.3.	Pirmas-trecias.\n" + // 7 group paragraph - 9 string
		                    "2. Antras."; // 8 group paragraph - 10 string

		private readonly STProcessor stProcessor;

		public TextProcessorTest() {
			stProcessor = new STProcessor();
		}
		 
		[TestMethod]
		public void TestGetArrayFromText () {
			TextProcessor textProcessor = new TextProcessor();
			var paragraphs = textProcessor.GetArrayFromText(text);
			Assert.AreEqual(11, paragraphs.Length);
		}

		[TestMethod]
		public void ParagraphsQuantity () {
			HashSet<Paragraph> paragraphs = stProcessor.GetParagraphsFromText(text);
			Assert.AreEqual(9, paragraphs.Count);
		}

		[TestMethod]
		public void FirstParTest () {
			HashSet<Paragraph> paragraphs = stProcessor.GetParagraphsFromText(text);
			var parList = paragraphs.ToList();
			Assert.AreEqual("Pirmas.", parList[0].Text);
		}

		[TestMethod]
		public void FirstParWithContinue () {
			HashSet<Paragraph> paragraphs = stProcessor.GetParagraphsFromText(text);
			var parList = paragraphs.ToList();
			Assert.AreEqual("Pirmas-antras.\nPirmas-antras continue", parList[2].Text);
		}

		[TestMethod]
		public void DeepInsertedParagraphContent () {
			HashSet<Paragraph> paragraphs = stProcessor.GetParagraphsFromText(text);
			var parList = paragraphs.ToList();
			Assert.AreEqual("Pirmas-antras-pirmas-ketvirtas.\nPirmas-antras-pirmas-ketvirtas continue.", parList[4].Text);
		}

		[TestMethod]
		public void SimpleParagraphChild () {
			HashSet<Paragraph> paragraphs = stProcessor.GetParagraphsFromText(text);
			var parList = paragraphs.ToList();
			Assert.AreEqual(parList[1].Text, parList[0].Paragraphs.ToList()[0].Text);
		}

		[TestMethod]
		public void DeepInsertedParagraphChild () {
			HashSet<Paragraph> paragraphs = stProcessor.GetParagraphsFromText(text);
			var parList = paragraphs.ToList();
			Assert.AreEqual(parList[5].Text, parList[2].Paragraphs.ToList()[1].Text);
		}

		[TestMethod]
		public void RegExTest () {
			string[] paragraphArray = stProcessor.GetArrayFromText(text);
			Match deepestParagraphMatch = stProcessor.MatchToParagraph(paragraphArray[8]);
			Assert.AreEqual("1", deepestParagraphMatch.Groups[1].ToString());
			Assert.AreEqual("2", deepestParagraphMatch.Groups[2].ToString());
			Assert.AreEqual("2", deepestParagraphMatch.Groups[3].ToString());
			Assert.AreEqual("1", deepestParagraphMatch.Groups[4].ToString());
			Assert.AreEqual("Pirmas-antras-antras-pirmas.", deepestParagraphMatch.Groups[6].ToString());
		}

		[TestMethod]
		public void splitStringToParagraphTest () {
			string[] paragraphArray = stProcessor.GetArrayFromText(text);
			bool isParagraph = stProcessor.splitStringToParagraph(paragraphArray[5]);
			Assert.AreEqual(true, isParagraph);
			bool isParagraph2 = stProcessor.splitStringToParagraph(paragraphArray[6]);
			Assert.AreEqual(false, isParagraph2);
		}
	}
}
