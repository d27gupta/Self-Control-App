﻿using PanCardView.Processors;
using PanCardView.Controls;
using System.Linq;
using System;

namespace PanCardView
{
	public class CarouselView : CardsView
	{
		public CarouselView() : this(new BaseCarouselFrontViewProcessor(), new BaseCarouselBackViewProcessor())
		{
		}

		public CarouselView(ICardProcessor frontViewProcessor, ICardProcessor backViewProcessor) : base(frontViewProcessor, backViewProcessor)
		{
			IsClippedToBounds = true;
			IsCyclical = true;
			MoveWidthPercentage = 0.3;
		}

		[Obsolete("No need use this property. Just add IndicatorsControl as child element.")]
		public bool ShouldAddDefaultIndicatorsControl
		{
			set
			{
				var control = Children.FirstOrDefault(c => c is IndicatorsControl);
				if (control != null)
				{
					Children.Remove(control);
				}

				if (value)
				{
					Children.Add(new IndicatorsControl());
				}
			}
		}
	}
}
