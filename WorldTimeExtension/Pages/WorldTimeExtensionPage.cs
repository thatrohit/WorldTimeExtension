// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

namespace WorldTimeExtension;

internal sealed partial class WorldTimeExtensionPage : ListPage
{
    public WorldTimeExtensionPage()
    {
        Icon = new IconInfo("🕑");
        Name = Title = "World Time";
        this.ShowDetails = true;

    }

    public override IListItem[] GetItems()
    {
        return [
            new ListItem(new NoOpCommand()) {
                Title = "Mumbai, India",
                Icon = new IconInfo(GetClockEmoji("India")),
                Subtitle = ConvertToTime("India", false),
                Details = new Details()
                {
                    Title = ConvertToTime("India", true),
                    HeroImage = IconHelpers.FromRelativePath("Assets\\ind.jpg")
                },
            },
            new ListItem(new NoOpCommand()) {
                Title = "Toronto, Canada",
                Icon = new IconInfo(GetClockEmoji("Toronto")),
                Subtitle = ConvertToTime("Toronto", false),
                Details = new Details()
                {
                    Title = ConvertToTime("Toronto", true),
                    HeroImage = IconHelpers.FromRelativePath("Assets\\toronto.jpg")
                },
            },
            new ListItem(new NoOpCommand()) {
                Title = "Melbourne, Australia",
                Icon = new IconInfo(GetClockEmoji("Melbourne")),
                Subtitle = ConvertToTime("Melbourne", false),
                Details = new Details()
                {
                    Title = ConvertToTime("Melbourne", true),
                    HeroImage = IconHelpers.FromRelativePath("Assets\\melb.jpg")
                },
            },
            new ListItem(new NoOpCommand()) {
                Title = "London, United Kingdom",
                Icon = new IconInfo(GetClockEmoji("London")),
                Subtitle = ConvertToTime("London", false),
                Details = new Details()
                {
                    Title = ConvertToTime("London", true),                    
                    HeroImage = IconHelpers.FromRelativePath("Assets\\london.jpg")
                },
            }
        ];
    }

    private string ConvertToTime(string timezone, Boolean isCompact)
    {
        DateTime localTime = DateTime.Now;
        TimeZoneInfo timeZoneInfo;

        switch (timezone)
        {
            case "India":
                timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                break;
            case "Toronto":
                timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                break;
            case "Melbourne":
                timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("AUS Eastern Standard Time");
                break;
            case "London":
                timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
                break;
            default:
                throw new ArgumentException("Invalid timezone");
        }

        DateTime convertedTime = TimeZoneInfo.ConvertTime(localTime, timeZoneInfo);
        if(!isCompact)
        {
            return $"{convertedTime:hh:mm tt}";
        } else
        {
            return $"{GetDayWithSuffix(convertedTime.Day)} of {convertedTime:MMMM yyyy}, {convertedTime:dddd}";
        }
    }

    private string GetClockEmoji(string timezone)
    {
        TimeZoneInfo timeZoneInfo;
        switch (timezone)
        {
            case "India":
                timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                break;
            case "Toronto":
                timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                break;
            case "Melbourne":
                timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("AUS Eastern Standard Time");
                break;
            case "London":
                timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
                break;
            default:
                throw new ArgumentException("Invalid timezone");
        }
        DateTime convertedTime = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
        int hour = convertedTime.Hour % 12;
        if (hour == 0) hour = 12;

        return hour switch
        {
            1 => "🕐",
            2 => "🕑",
            3 => "🕒",
            4 => "🕓",
            5 => "🕔",
            6 => "🕕",
            7 => "🕖",
            8 => "🕗",
            9 => "🕘",
            10 => "🕙",
            11 => "🕚",
            12 => "🕛",
            _ => "🕛"
        };
    }

    private string GetDayWithSuffix(int day)
    {
        switch (day)
        {
            case 1:
            case 21:
            case 31:
                return day + "st";
            case 2:
            case 22:
                return day + "nd";
            case 3:
            case 23:
                return day + "rd";
            default:
                return day + "th";
        }
    }
}
