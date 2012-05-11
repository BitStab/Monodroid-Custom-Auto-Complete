/*
 * Copyright (C) 2011 The Android Open Source Project
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

namespace iNAV 
{
	public class Directory 
	{
		private static DirectoryCategory [] mCategories;
		
		public static void InitializeDirectory () 
		{
			mCategories = new DirectoryCategory [] {
				new DirectoryCategory ("Mobile Sales", new DirectoryEntry [] {
					new DirectoryEntry ("Inserire Ordine", Resource.Layout.Dashboard),
					new DirectoryEntry ("Inserire Reso", Resource.Drawable.green_balloon),
					new DirectoryEntry ("Situazione Provvigionale", Resource.Drawable.blue_balloon),
					new DirectoryEntry ("Promozioni Prezzi Sconti", Resource.Drawable.blue_balloon),
					new DirectoryEntry ("Estratto Conto Cliente", Resource.Drawable.blue_balloon),
					new DirectoryEntry ("Raccolta lamentele/richieste", Resource.Drawable.blue_balloon),
					new DirectoryEntry ("Pianifica percorso", Resource.Drawable.blue_balloon)
					}),
				};
		}
		
		public static int CategoryCount {
			get { return mCategories.Length; }
		}
		
		public static DirectoryCategory GetCategory (int i)
		{
			return mCategories[i];
		}
	}
}