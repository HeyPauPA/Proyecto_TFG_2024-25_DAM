using UnityEngine;
using TMPro;
using System;

namespace SimpleCalendar
{
    /// <summary>
    /// Button that fills the day grid panel
    /// </summary>
    public class DayButton : MonoBehaviour
    {
        [Tooltip("reference to the text field that displays the day.")]
        [SerializeField] private TextMeshProUGUI dayText;

        [Tooltip("Color of the day's box when it is today's date.")]
        [SerializeField] private Color todaysColor = Color.cyan;

        [Tooltip("Base Color of the day's box.")]
        [SerializeField] private Color baseColor = Color.white;

        [Tooltip("Alternate Color of the day's box.")]
        [SerializeField] private Color altColor = new(0.90f, 0.90f, 0.90f);

        [Tooltip("Color of the day's box when it is not part of the selected month (AKA a padding day).")]
        [SerializeField] private Color paddingDayColor = new(0.75f, 0.75f, 0.75f);


        /// <summary>
        /// The date associated with this day button
        /// </summary>
        private DateTime myDate;

        /// <summary>
        /// Sets the myDate variable, changes the daytext, and set the color of the button based on the parameters
        /// </summary>
        public void SetDay(DateTime day, bool curMonth, int col)
        {
            myDate = day;
            dayText.text = myDate.Day.ToString();

            // Set the color:
            if (myDate.Date == DateTime.Now.Date)
            {
                GetComponent<UnityEngine.UI.Image>().color = todaysColor;
            }
            else if (!curMonth)
            {
                GetComponent<UnityEngine.UI.Image>().color = paddingDayColor;
            }
            else
            {
                if (col % 2 == 0)
                {
                    GetComponent<UnityEngine.UI.Image>().color = baseColor;
                }
                else
                {
                    GetComponent<UnityEngine.UI.Image>().color = altColor;
                }
            }
        }
    }
}