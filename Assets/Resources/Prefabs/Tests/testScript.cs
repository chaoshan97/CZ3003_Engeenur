using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Tests
{
    public class NewTestScript
    {
        private BattleModelViewScript battleModelViewScript;
        private GameObject battleCanvas;
        [SetUp]
        public void Setup()
        {
            battleCanvas = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/BattleCanvas"));
            battleModelViewScript = battleCanvas.GetComponent<BattleModelViewScript>();
            battleModelViewScript.monsterPrefab = (Resources.Load<GameObject>("Prefabs/Monster"));
            battleModelViewScript.playerPrefab = (Resources.Load<GameObject>("Prefabs/Player"));
            battleModelViewScript.init();
        }
        // A Test behaves as an ordinary method
        [Test]
        public void TestCorrectAnswerMonsterTakeDamage()
        {
            // Use the Assert class to test conditions
            
            InputField input = battleCanvas.GetComponentInChildren<InputField>();
            battleModelViewScript.Answer = 5;
            battleModelViewScript.answerInput.text = "5";
            battleModelViewScript.MonsterHealth = 10000;
            int initialMonsterHealth = battleModelViewScript.MonsterHealth;
            battleModelViewScript.checkAnswer();
            Assert.Less(battleModelViewScript.MonsterHealth, initialMonsterHealth);
        }

        [Test]
        public void TestCorrectAnswerQuestionChange()
        {
            // Use the Assert class to test conditions

            InputField input = battleCanvas.GetComponentInChildren<InputField>();
            battleModelViewScript.Answer = 5;
            battleModelViewScript.answerInput.text = "5";
            string initialQuestion = battleModelViewScript.QuestionString;
            battleModelViewScript.checkAnswer();
            Assert.AreNotEqual(battleModelViewScript.QuestionString, initialQuestion);
        }

        [Test]
        public void TestWrongAnswerPlayerTakeDamage()
        {
            // Use the Assert class to test conditions

            InputField input = battleCanvas.GetComponentInChildren<InputField>();
            battleModelViewScript.Answer = 5;
            battleModelViewScript.answerInput.text = "4";
            int initialPlayerHealth = battleModelViewScript.PlayerHealth;
            battleModelViewScript.checkAnswer();
            Assert.Less(battleModelViewScript.PlayerHealth, initialPlayerHealth);
        }

        [Test]
        public void TestWrongAnswerQuestionChange()
        {
            // Use the Assert class to test conditions

            InputField input = battleCanvas.GetComponentInChildren<InputField>();
            battleModelViewScript.Answer = 5;
            battleModelViewScript.answerInput.text = "4";
            string initialQuestion = battleModelViewScript.QuestionString;
            battleModelViewScript.checkAnswer();
            Assert.AreNotEqual(battleModelViewScript.QuestionString, initialQuestion);
        }

        [Test]
        public void TestOpenResultScreenWhenMonsterHealthIsZero()
        {
            // Use the Assert class to test conditions

            InputField input = battleCanvas.GetComponentInChildren<InputField>();
            battleModelViewScript.Answer = 5;
            battleModelViewScript.answerInput.text = "5";
            battleModelViewScript.MonsterHealth = 0;
            battleModelViewScript.checkAnswer();
            
            Assert.True(battleModelViewScript.resultUIScript.gameObject.activeSelf);
        }

        [Test]
        public void TestOpenResultScreenWhenPlayerHealthIsZero()
        {
            // Use the Assert class to test conditions

            InputField input = battleCanvas.GetComponentInChildren<InputField>();
            battleModelViewScript.Answer = 5;
            battleModelViewScript.answerInput.text = "5";
            battleModelViewScript.PlayerHealth = 0;
            battleModelViewScript.checkAnswer();

            Assert.True(battleModelViewScript.resultUIScript.gameObject.activeSelf);
        }
        
        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator NewTestScriptWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
