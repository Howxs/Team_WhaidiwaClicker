using UnityEngine;

public class KeepAudio : MonoBehaviour
{
    void Awake()
    {
        // สั่งห้ามทำลายวัตถุนี้เมื่อเปลี่ยน Scene
        DontDestroyOnLoad(transform.gameObject);
    }

    public void PlaySound()
    {
        GetComponent<AudioSource>().Play();

        // (Optionเสริม) สั่งทำลายตัวเองทิ้งหลังจากเสียงจบ 2 วินาที 
        // เพื่อไม่ให้ขยะรก Memory ในฉากหน้า
        Destroy(gameObject, 1.0f);
    }

}