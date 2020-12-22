using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRelicPickupAnimation : UIAnimationBase
{
    public Image m_maskImage;
    public Image m_image;
    public Image m_rarityFrame;

    public override void PlayAnim()
    {
        m_maskImage.color = new Color(m_maskImage.color.r, m_maskImage.color.g, m_maskImage.color.b, 0.4f);
        m_image.color = new Color(m_image.color.r, m_image.color.g, m_image.color.b, 0.4f);
        m_rarityFrame.color = new Color(m_rarityFrame.color.r, m_rarityFrame.color.g, m_rarityFrame.color.b, 0.4f);

        transform.localScale = new Vector3(2, 2, 2);

        m_shouldAnimate = true;
    }

    void Update()
    {
        if (!m_shouldAnimate)
        {
            return;
        }

        transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(1, 1, 1), 0.1f);

        m_maskImage.color = new Color(m_maskImage.color.r, m_maskImage.color.g, m_maskImage.color.b, m_maskImage.color.a + 0.05f);
        m_image.color = new Color(m_image.color.r, m_image.color.g, m_image.color.b, m_image.color.a + 0.05f);
        m_rarityFrame.color = new Color(m_rarityFrame.color.r, m_rarityFrame.color.g, m_rarityFrame.color.b, m_rarityFrame.color.a + 0.05f);

        if (transform.localScale == new Vector3(1,1,1))
        {
            m_shouldAnimate = false;
        }
    }
}
