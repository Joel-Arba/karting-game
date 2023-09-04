using UnityEngine;
using UnityEngine.UI;

public class ScrollSync : MonoBehaviour
{
    public ScrollRect otherScrollRect;

    public ScrollRect scrollRect;

    private void Update()
    {
        if(scrollRect.velocity != Vector2.zero)
        {
            otherScrollRect.verticalNormalizedPosition = scrollRect.verticalNormalizedPosition;
        }
        else if(otherScrollRect.velocity != Vector2.zero)
        {
            scrollRect.verticalNormalizedPosition = otherScrollRect.verticalNormalizedPosition;
        }
        
    }
}
