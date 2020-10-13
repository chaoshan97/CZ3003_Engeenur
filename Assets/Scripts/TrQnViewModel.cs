using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using Proyecto26;

public class TrQnViewModel : MonoBehaviour
{
    public static string qns;
    public static string ans;
    public static int level;

    public string userName = "Alice";
    public InputField qnsCreateInput;
    public InputField ansCreateInput;
    public InputField qnsEditInput;
    public InputField ansEditInput;

    public GameObject itemParent, item, formCreate, formUpdate;
    // Start is called before the first frame update
    //void Start()
    //{
    //    read();
    //}

    //public void read()
    //{
    //    for (int i = 0; i < itemParent.transform.childCount; i++)
    //    {
    //        Destroy(itemParent.transform.GetChild(i).gameObject);
    //    }
    //    DatabaseQAHandler.GetQns(qns =>
    //    {
    //        int number = 0;
    //        foreach (var qn in qns)
    //        {
    //            Debug.Log($"{qn.Key} {qn.Value.qns} {qn.Value.ans} {qn.Value.level} {qn.Value.userName}");
    //            if (qn.Value.userName == userName)
    //            {
    //                GameObject tmp_item = Instantiate(item, itemParent.transform);
    //                tmp_item.name = qn.Key;
    //                tmp_item.transform.GetChild(0).GetComponent<Text>().text = number.ToString();
    //                tmp_item.transform.GetChild(1).GetComponent<Text>().text = qn.Value.qns;
    //                tmp_item.transform.GetChild(2).GetComponent<Text>().text = qn.Value.ans;
    //                number++;
    //            }
    //        }
    //    });
    //}

    //public void create()
    //{
    //    var qn = new Qn(qnsCreateInput.text, ansCreateInput.text, 1, userName);
    //    DatabaseQAHandler.PostQn(qn, () => { });
    //    formCreate.transform.GetChild(1).GetComponent<InputField>().text = "";
    //    formCreate.transform.GetChild(2).GetComponent<InputField>().text = "";
    //    read();
    //}

    //public void delete(GameObject item)
    //{
    //    string qnKey = item.name;
    //    DatabaseQAHandler.DeleteQn(qnKey, () => { });
    //    read();
    //}

    //public string qnKey;
    //public void openFormEdit(GameObject obj_edit)
    //{
    //    qnKey = obj_edit.name;
    //    formUpdate.SetActive(true);
    //}

    //public void update()
    //{
    //    Debug.Log("Qns: " + qnKey);
    //    DatabaseQAHandler.GetQn(qnKey, qn =>
    //    {
    //        Debug.Log($"{qn.qns} {qn.ans} {qn.level} {qn.userName}");
    //    });
    //    var question = new Qn(qnsEditInput.text, ansEditInput.text, 1, userName);
    //    RestClient.Put("https://engeenur-17baa.firebaseio.com/qns/" + qnKey + ".json", question);
    //    read();
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //    //DatabaseQAHandler.PutQn(qnKey, qn,  () => { });
    //    //waiter();
    //    //IEnumerator waiter(){ yield return new WaitForSeconds(10);}
    //}
    //public void UpdateQ()
    //{
    //    var qn = new Qn(qnsEditInput.text, ansEditInput.text, 1, userName);
    //    DatabaseQAHandler.PostQn(qn, () => { });
    //    formUpdate.transform.GetChild(1).GetComponent<InputField>().text = "";
    //    formUpdate.transform.GetChild(2).GetComponent<InputField>().text = "";
    //    read();
    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
}
