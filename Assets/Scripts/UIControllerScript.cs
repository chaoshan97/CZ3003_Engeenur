using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControllerScript : MonoBehaviour
{
    public GameObject loginCanvas;
    public GameObject loginTrCanvas;
    public GameObject createAccountCanvas;
    public GameObject townCanvas;
    public GameObject inventoryCanvas;
    public GameObject levelSelectCanvas;
    public GameObject shopCanvas;
    public GameObject battleCanvas;
    public GameObject shopScrollView;
    public GameObject inventoryScrollView;
    public GameObject questionUI;
    public GameObject battleMenu;
    public GameObject friendCanvas;
    public GameObject leadershipBoardCanvas;
    public GameObject teacherTownCanvas;
    public GameObject questionCanvas;
    public GameObject viewResultsCanvas;
    public GameObject courseCanvas;
    public GameObject specialLevelCanvas;
    public GameObject trQnCanvas;
    public GameObject enrollCanvas;
    public GameObject mainCanvas;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void loginButton()
    {
        loginCanvas.SetActive(false);
        townCanvas.SetActive(true);

    }

    public void teacherLoginButton()
    {
        loginTrCanvas.SetActive(false);
        teacherTownCanvas.SetActive(true);

    }

    public void gotoCreateAccountButton()
    {
        loginCanvas.SetActive(false);
        createAccountCanvas.SetActive(true);

    }

    public void createAccount()
    {
        createAccountCanvas.SetActive(false);
        townCanvas.SetActive(true);

    }

    public void OpenIntentoryButton()
    {
        townCanvas.SetActive(false);
        inventoryCanvas.SetActive(true);
    }

    public void CloseIntentoryButton()
    {
        inventoryCanvas.SetActive(false);
        townCanvas.SetActive(true);
    }

    public void OpenLevelSelectButton()
    {
        townCanvas.SetActive(false);
        levelSelectCanvas.SetActive(true);
    }

    public void CloseLevelSelectButton()
    {
        levelSelectCanvas.SetActive(false);
        townCanvas.SetActive(true);
    }

    public void OpenShopButton()
    {
        townCanvas.SetActive(false);
        shopCanvas.SetActive(true);
    }

    public void CloseShopButton()
    {
        inventoryScrollView.SetActive(false);
        shopScrollView.SetActive(false);
        shopCanvas.SetActive(false);
        townCanvas.SetActive(true);
    }

    public void OpenFriendListButton()
    {
        townCanvas.SetActive(false);
        friendCanvas.SetActive(true);
    }

    public void CloseFriendListButton()
    {
        friendCanvas.SetActive(false);
        townCanvas.SetActive(true);
    }

    public void OpenTeacherFriendListButton()
    {
        teacherTownCanvas.SetActive(false);
        friendCanvas.SetActive(true);
    }

    public void CloseTeacherFriendListButton()
    {
        friendCanvas.SetActive(false);
        teacherTownCanvas.SetActive(true);
    }

    public void OpenLeadershipBoardButton()
    {
        townCanvas.SetActive(false);
        leadershipBoardCanvas.SetActive(true);
    }

    public void OpenTeacherLeadershipBoardButton()
    {
        teacherTownCanvas.SetActive(false);
        leadershipBoardCanvas.SetActive(true);
    }

    public void CloseTeacherLeadershipBoardButton()
    {
        leadershipBoardCanvas.SetActive(false);
        teacherTownCanvas.SetActive(true);
    }

    public void CloseLeadershipBoardButton()
    {
        leadershipBoardCanvas.SetActive(false);
        townCanvas.SetActive(true);

    }

    public void OpenShopScrollView()
    {
        inventoryScrollView.SetActive(false);
        shopScrollView.SetActive(true);
    }

    public void OpenInventoryScrollView()
    {
        inventoryScrollView.SetActive(true);
        shopScrollView.SetActive(false);
    }

    public void OpenQuestionUI()
    {
        questionUI.SetActive(true);
        battleMenu.SetActive(false);
    }

    public void OpenBattleCanvas()
    {
        battleCanvas.SetActive(true);
        levelSelectCanvas.SetActive(false);
    }

    public void OpenCreateLevelCanvas()
    {
        questionCanvas.SetActive(true);
        teacherTownCanvas.SetActive(false);
    }

    public void OpenViewResultsCanvas()
    {
        viewResultsCanvas.SetActive(true);
        teacherTownCanvas.SetActive(false);
    }
    public void EnrollmentToCourseCanvas()
    {
        enrollCanvas.SetActive(false);
        courseCanvas.SetActive(true);
    }

    public void CourseToEnrollmentCanvas()
    {
        courseCanvas.SetActive(false);
        enrollCanvas.SetActive(true);

    }
    public void EnrollmentToSpecialLevelCanvas()
    {
        enrollCanvas.SetActive(false);
        specialLevelCanvas.SetActive(true);
    }

    public void SpecialLevelToEnrollmentCanvas()
    {
        specialLevelCanvas.SetActive(false);
        enrollCanvas.SetActive(true);
    }
    public void OpenTrQnCanvas()
    {
        specialLevelCanvas.SetActive(false);
        trQnCanvas.SetActive(true);
    }
    public void CloseTrQnCanvas()
    {
        trQnCanvas.SetActive(false);
        specialLevelCanvas.SetActive(true);
    }
    public void TrQnToCourseCanvas()
    {
        trQnCanvas.SetActive(false);
        courseCanvas.SetActive(true);
    }

    public void CloseBattleCanvas()
    {
        battleCanvas.SetActive(false);
        townCanvas.SetActive(true);
    }

    public void ProceedToLoginCanvas()
    {
        mainCanvas.SetActive(false);
        loginCanvas.SetActive(true);
    }

    public void ProceedToCreateAccCanvas()
    {
        mainCanvas.SetActive(false);
        createAccountCanvas.SetActive(true);
    }

    public void LoginToMainCanvas()
    {
        loginCanvas.SetActive(false);
        mainCanvas.SetActive(true);
    }
    public void LoginTrToMainCanvas()
    {
        loginTrCanvas.SetActive(false);
        mainCanvas.SetActive(true);
    }

    public void CreateAccToMainCanvas()
    {
        createAccountCanvas.SetActive(false);
        mainCanvas.SetActive(true);
    }

}
