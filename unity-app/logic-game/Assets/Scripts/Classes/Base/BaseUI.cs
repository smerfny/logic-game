using UnityEngine;

namespace Assets.Scripts.Classes.Base
{
    public class BaseUI
    {
        protected GameObject menuObject;

        public BaseUI(GameObject menuObject)
        {
            this.menuObject = menuObject;
        }

        protected T GetReference<T>(string name)
        {
            return menuObject.transform.Find(name).gameObject.GetComponent<T>();
        }

        public void Show()
        {
            menuObject.SetActive(true);
        }

        public void Hide()
        {
            menuObject.SetActive(false);
        }
    }
}
