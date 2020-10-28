using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using System.Runtime.ExceptionServices;
using UnityEngine.SceneManagement;
using System;

namespace Tests
{
    public class TestSuite
    {

        // A Test behaves as an ordinary method
        [Test]
        public void TestSuiteSimplePasses()
        {
            // Use the Assert class to test conditions
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator TestSuiteWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }

        public static GameObject courseCanvas = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/CourseCanvas"));
        // public TrCourseViewModel trCourseViewModel = courseCanvas.transform.Find("CourseViewModel").GetComponent<TrCourseViewModel>();



        [OneTimeSetUp]

        public void LoadScene()
        {
            SceneManager.LoadScene("MainSceneIntergated2");
        }

        [UnityTest]
        public IEnumerator CreateCourseNotExist()
        {
            GameObject courseCanvas = GameObject.Find("CourseCanvas");
            TrCourseViewModel trCourseViewModel = courseCanvas.transform.Find("CourseViewModel").GetComponent<TrCourseViewModel>();
            trCourseViewModel.userName = "Mary";
            trCourseViewModel.courseInput.text = "MathTester1000";
            trCourseViewModel.CreateCourse();
            yield return new WaitForSeconds(30);
            Assert.IsTrue(trCourseViewModel.created);
        }

        [UnityTest]
        public IEnumerator CreateCourseAlrExist()
        {   //GameObject loader = courseCanvas.transform.Find("loader").GetComponent<GameObject>();
            GameObject courseCanvas = GameObject.Find("CourseCanvas");
            TrCourseViewModel trCourseViewModel = courseCanvas.transform.Find("CourseViewModel").GetComponent<TrCourseViewModel>();
            trCourseViewModel.userName = "Mary";
            trCourseViewModel.courseInput.text = "Math100";
            trCourseViewModel.CreateCourse();
            yield return new WaitForSeconds(30);
            Assert.IsFalse(trCourseViewModel.created);
        }
 
        [UnityTest]
        public IEnumerator EnrollValidStud()
        {
            GameObject enrollCanvas = GameObject.Find("EnrollmentCanvas");
            EnrollViewModel enrollViewModel = enrollCanvas.transform.Find("EnrollViewModel").GetComponent<EnrollViewModel>();
            enrollViewModel.userName = "Mary";
            enrollViewModel.courseName = "Math100";
            enrollViewModel.studInput.text = "tanbp";
            enrollViewModel.Read();
            enrollViewModel.CreateStudEnroll();
            yield return new WaitForSeconds(10);
            Assert.IsTrue(enrollViewModel.created);
        }

        [UnityTest]
        public IEnumerator EnrollInvalidStud()
        {
            GameObject enrollCanvas = GameObject.Find("EnrollmentCanvas");
            EnrollViewModel enrollViewModel = enrollCanvas.transform.Find("EnrollViewModel").GetComponent<EnrollViewModel>();
            enrollViewModel.userName = "Mary";
            enrollViewModel.courseName = "Math100";
            enrollViewModel.studInput.text = "student999";
            enrollViewModel.Read();
            enrollViewModel.CreateStudEnroll();
            yield return new WaitForSeconds(10);
            Assert.IsFalse(enrollViewModel.created);
        }

        [UnityTest]
        public IEnumerator EnrollEnrolledStud()
        {
            GameObject enrollCanvas = GameObject.Find("EnrollmentCanvas");
            EnrollViewModel enrollViewModel = enrollCanvas.transform.Find("EnrollViewModel").GetComponent<EnrollViewModel>();
            enrollViewModel.userName = "Mary";
            enrollViewModel.courseName = "Math100";
            enrollViewModel.studInput.text = "";
            enrollViewModel.Read();
            enrollViewModel.CreateStudEnroll();
            yield return new WaitForSeconds(10);
            Assert.IsFalse(enrollViewModel.created);
        }

        [UnityTest]
        public IEnumerator CreateLvl()
        {
            GameObject specialCanvas = GameObject.Find("SpecialLevelCanvas");
            TrSpecialLvlViewModel trSpecialLvlViewModel = specialCanvas.transform.Find("SpecialLevelViewModel").GetComponent<TrSpecialLvlViewModel>();
            trSpecialLvlViewModel.userName = "Mary";
            trSpecialLvlViewModel.courseName = "Math100";
            trSpecialLvlViewModel.lvlNoIF = 2;
            trSpecialLvlViewModel.Read();
            trSpecialLvlViewModel.CreateLvl();
            yield return new WaitForSeconds(10);
            Assert.IsTrue(trSpecialLvlViewModel.created);
        }

        [UnityTest]
        public IEnumerator CreateLvlAlrExist()
        {
            GameObject specialCanvas = GameObject.Find("SpecialLevelCanvas");
            TrSpecialLvlViewModel trSpecialLvlViewModel = specialCanvas.transform.Find("SpecialLevelViewModel").GetComponent<TrSpecialLvlViewModel>();
            trSpecialLvlViewModel.userName = "Mary";
            trSpecialLvlViewModel.courseName = "Math100";
            trSpecialLvlViewModel.lvlNoIF = 1;
            trSpecialLvlViewModel.Read();
            trSpecialLvlViewModel.CreateLvl();
            yield return new WaitForSeconds(10);
            Assert.IsFalse(trSpecialLvlViewModel.created);
        }

    }
}
