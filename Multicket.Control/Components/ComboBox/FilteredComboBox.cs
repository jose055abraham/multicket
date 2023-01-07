using System.Collections;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Multicket.Module.Components
{
    public class FilteredComboBox : ComboBox
    {
        ////
        // Public Fields
        ////

        /// <summary>
        /// The search string treshold length.
        /// </summary>
        /// <remarks>
        /// It's implemented as a Dependency Property, so you can Set it in a XAML template 
        /// </remarks>
        public static readonly DependencyProperty MinimumSearchLengthProperty =
            DependencyProperty.Register(
                "MinimumSearchLength",
                typeof(int),
                typeof(FilteredComboBox),
                new UIPropertyMetadata(3));

        ////
        // Private Fields
        //// 

        /// <summary>
        /// Caches the previous value of the filter.
        /// </summary>
        private string oldFilter = string.Empty;

        /// <summary>
        /// Holds the current value of the filter.
        /// </summary>
        private string currentFilter = string.Empty;

        ////
        // Constructors
        //// 

        /// <summary>
        /// Initializes a new instance of the FilteredComboBox class.
        /// </summary>
        /// <remarks>
        /// You could Set 'IsTextSearchEnabled' to 'false' here,
        /// to avoid non-intuitive behavior of the control
        /// </remarks>
        public FilteredComboBox()
        {
        }

        ////
        // Properties
        //// 

        /// <summary>
        /// Gets or sets the search string treshold length.
        /// </summary>
        /// <value>The minimum length of the search string that triggers filtering.</value>
        [Description("Length of the search string that triggers filtering.")]
        [Category("Filtered ComboBox")]
        [DefaultValue(3)]
        public int MinimumSearchLength
        {
            [System.Diagnostics.DebuggerStepThrough]
            get => (int)GetValue(MinimumSearchLengthProperty);

            [System.Diagnostics.DebuggerStepThrough]
            set => SetValue(MinimumSearchLengthProperty, value);
        }

        /// <summary>
        /// Gets a reference to the internal editable textbox.
        /// </summary>
        /// <value>A reference to the internal editable textbox.</value>
        /// <remarks>
        /// We need this to Get access to the Selection.
        /// </remarks>
        protected TextBox EditableTextBox => GetTemplateChild("PART_EditableTextBox") as TextBox;


        ////
        // Event Raiser Overrides
        //// 

        /// <summary>
        /// Keep the filter if the ItemsSource is explicitly changed.
        /// </summary>
        /// <param name="oldValue">The previous value of the filter.</param>
        /// <param name="newValue">The current value of the filter.</param>
        protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            if (newValue != null)
            {
                ICollectionView view = CollectionViewSource.GetDefaultView(newValue);
                view.Filter += FilterPredicate;
            }

            if (oldValue != null)
            {
                ICollectionView view = CollectionViewSource.GetDefaultView(oldValue);
                view.Filter -= FilterPredicate;
            }

            base.OnItemsSourceChanged(oldValue, newValue);
        }

        /// <summary>
        /// Confirm or cancel the selection when Tab, Enter, or Escape are hit. 
        /// Open the DropDown when the Down Arrow is hit.
        /// </summary>
        /// <param name="e">Key Event Args.</param>
        /// <remarks>
        /// The 'KeyDown' event is not raised for Arrows, Tab and Enter keys.
        /// It is swallowed by the DropDown if it's open.
        /// So use the Preview instead.
        /// </remarks>
        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
                // Explicit Selection -> Close ItemsPanel
                IsDropDownOpen = false;
            }
            else if (e.Key == Key.Escape)
            {
                // Escape -> Close DropDown and redisplay Filter
                IsDropDownOpen = false;
                SelectedIndex = -1;
                Text = currentFilter;
            }
            else
            {
                if (e.Key == Key.Down)
                {
                    // Arrow Down -> Open DropDown
                    IsDropDownOpen = true;
                }

                base.OnPreviewKeyDown(e);
            }

            // Cache text
            oldFilter = Text;
        }

        /// <summary>
        /// Modify and apply the filter.
        /// </summary>
        /// <param name="e">Key Event Args.</param>
        /// <remarks>
        /// Alternatively, you could react on 'OnTextChanged', but navigating through 
        /// the DropDown will also change the text.
        /// </remarks>
        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (e.Key == Key.Up || e.Key == Key.Down)
            {
                // Navigation keys are ignored
            }
            else if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
                // Explicit Select -> Clear Filter
                ClearFilter();
            }
            else
            {
                // The text was changed
                if (Text != oldFilter)
                {
                    // Clear the filter if the text is empty,
                    // apply the filter if the text is long enough
                    if (Text.Length == 0 || Text.Length >= MinimumSearchLength)
                    {
                        RefreshFilter();
                        IsDropDownOpen = true;

                        // Unselect
                        EditableTextBox.SelectionStart = int.MaxValue;
                    }
                }

                base.OnKeyUp(e);

                // Update Filter Value
                currentFilter = Text;
            }
        }

        /// <summary>
        /// Make sure the text corresponds to the selection when leaving the control.
        /// </summary>
        /// <param name="e">A KeyBoardFocusChangedEventArgs.</param>
        protected override void OnPreviewLostKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            ClearFilter();
            int temp = SelectedIndex;
            SelectedIndex = -1;
            Text = string.Empty;
            SelectedIndex = temp;
            base.OnPreviewLostKeyboardFocus(e);
        }

        ////
        // Helpers
        ////

        /// <summary>
        /// Re-apply the Filter.
        /// </summary>
        private void RefreshFilter()
        {
            if (ItemsSource != null)
            {
                ICollectionView view = CollectionViewSource.GetDefaultView(ItemsSource);
                view.Refresh();
            }
        }

        /// <summary>
        /// Clear the Filter.
        /// </summary>
        private void ClearFilter()
        {
            currentFilter = string.Empty;
            RefreshFilter();
        }

        /// <summary>
        /// The Filter predicate that will be applied to each row in the ItemsSource.
        /// </summary>
        /// <param name="value">A row in the ItemsSource.</param>
        /// <returns>Whether or not the item will appear in the DropDown.</returns>
        private bool FilterPredicate(object value)
        {
            // No filter, no text
            if (value == null)
            {
                return false;
            }

            // No text, no filter
            if (Text.Length == 0)
            {
                return true;
            }

            // Case insensitive search
            return value.ToString().ToLower().Contains(Text.ToLower());
        }
    }
}
