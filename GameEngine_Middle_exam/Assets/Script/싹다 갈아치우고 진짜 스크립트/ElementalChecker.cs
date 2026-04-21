using UnityEngine;

public class ElementalChecker : MonoBehaviour
{
    public enum Element
    {
        Fire,
        Nature,
        Water,
        Light,
        Wind
    }

    public Element currentElement;

    public bool CanGrapple()
    {
        return isNature;
    }

    public bool isFire => currentElement == Element.Fire;
    public bool isNature => currentElement == Element.Nature;
    public bool isWater => currentElement == Element.Water;
    public bool isLight => currentElement == Element.Light;
    public bool isWind => currentElement == Element.Wind;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SetElement(Element.Fire);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SetElement(Element.Nature);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SetElement(Element.Water);
        if (Input.GetKeyDown(KeyCode.Alpha4)) SetElement(Element.Light);
        if (Input.GetKeyDown(KeyCode.Alpha5)) SetElement(Element.Wind);

        if (Input.GetKeyDown(KeyCode.Q)) PrevElement();
        if (Input.GetKeyDown(KeyCode.E)) NextElement();
    }

    void SetElement(Element newElement)
    {
        currentElement = newElement;
        Debug.Log("현재 속성: " + currentElement);
    }

    void NextElement()
    {
        currentElement = (Element)(((int)currentElement + 1) % 5);
        Debug.Log("현재 속성: " + currentElement);
    }

    void PrevElement()
    {
        currentElement = (Element)(((int)currentElement - 1 + 5) % 5);
        Debug.Log("현재 속성: " + currentElement);
    }
    //bool값 뺑뺑이보단 0부터 4의 int 값으로 스킬의 상태 뺑뺑이 돌리기 ㄹㅇ 개쩌는 아이디어
}
