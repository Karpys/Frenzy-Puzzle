namespace PuzzleFrenzy.Scripts
{
    using TMPro;
    using UnityEngine;

    public class TextDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_Text = null;

        public void Initialize(string text)
        {
            m_Text.text = text;
        }
    }
}