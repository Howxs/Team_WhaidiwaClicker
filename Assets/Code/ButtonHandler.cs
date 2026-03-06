using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    // Function นี้จะถูกเรียกเมื่อกดปุ่ม
    public void HideSelf()
    {
        // gameObject ในที่นี้คือตัวปุ่มเองที่ Script นี้เกาะอยู่
        this.gameObject.SetActive(false);
    }
    public void OpenSelf()
    {
        this.gameObject.SetActive(true);
    }
}