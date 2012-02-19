/*
 * Copyright © Eric Maupin 2012
 * This file is part of Alloy.
 *
 * Alloy is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * Alloy is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with Alloy.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Windows.Input;

namespace Alloy.Clients.Windows
{
	internal class DelegatedCommand<T>
		: DelegatedCommand
	{
		public DelegatedCommand (Action<T> execute)
			: base (s => execute ((T)s))
		{
			if (execute == null)
				throw new ArgumentNullException ("execute");
		}

		public DelegatedCommand (Action<T> execute, Func<T, bool> canExecute)
			: base (s => execute ((T)s), s => canExecute ((T)s))
		{
			if (execute == null)
				throw new ArgumentNullException ("execute");
			if (canExecute == null)
				throw new ArgumentNullException ("canExecute");
		}
	}

	internal class DelegatedCommand
		: ICommand
	{
		public DelegatedCommand (Action<object> execute)
			: this (execute, s => true)
		{
		}

		public DelegatedCommand (Action<object> execute, Func<object, bool> canExecute)
		{
			if (execute == null)
				throw new ArgumentNullException ("execute");
			if (canExecute == null)
				throw new ArgumentNullException ("canExecute");

			this.execute = execute;
			this.canExecute = canExecute;
		}

		public event EventHandler CanExecuteChanged;

		public void Execute (object parameter)
		{
			this.execute (parameter);
		}

		public bool CanExecute (object parameter)
		{
			return this.canExecute (parameter);
		}

		public void NotifyExecutabilityChanged()
		{
			var changed = CanExecuteChanged;
			if (changed != null)
				changed (this, EventArgs.Empty);
		}

		private readonly Action<object> execute;
		private readonly Func<object, bool> canExecute;
	}
}