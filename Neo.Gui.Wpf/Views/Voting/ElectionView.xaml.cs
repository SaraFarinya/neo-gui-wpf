﻿using Neo.Gui.Base.Dialogs.Interfaces;
using Neo.Gui.Base.Dialogs.Results;

namespace Neo.Gui.Wpf.Views.Voting
{
    public partial class ElectionView : IDialog<ElectionDialogResult>
    {
        public ElectionView()
        {
            InitializeComponent();
        }
    }
}