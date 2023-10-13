## TimeBlock

A C# library for creating and managing time blocks.

### Features

* Create time blocks that match specific dates and times, or ranges of dates and times.
* Use a variety of unit types, including seconds, minutes, hours, days, months, and years.
* Combine units to create complex time blocks, such as all times on weekdays between 12:00 PM and 2:00 PM.
* Check if a specific date and time matches a time block.
* Parse time block specifications from strings.

### Usage

To use the TimeBlock library, you can either create a new `TimeBlock` object directly, or parse a time block specification from a string using the `TimeBlockParser` class.

```c#
// Create a TimeBlock object directly.
var weekdayLunchtime = new TimeBlock(
    hours: new IntervalUnit(30),
    days: new SingleUnit(1, 2, 3, 4, 5)
);

// Parse a time block specification from a string.
var timeBlock = TimeBlockParser.Parse("* * 12-14 Mon-Fri * *"); Every Mon-Fri between 1200-1400
```

Once you have a `TimeBlock` object, you can use the `IsMatch()` method to check if a specific date and time matches the time block.

```c#
// Check if the current date and time matches the weekday lunchtime time block.
bool isLunchtime = weekdayLunchtime.IsMatch(DateTime.Now);
```

### TimeBlock specification format

The TimeBlock specification format is a sequence of 6 units, separated by spaces. The units are as follows:

* Seconds (0-59)
* Minutes (0-59)
* Hours (0-23)
* Days of the week (Sun, Mon, Tue, Wed, Thu, Fri, Sat or 0-6)
* Months of the year (Jan, Feb, Mar, Apr, May, Jun, Jul, Aug, Sep, Oct, Nov, Dec or 1-12)
* Years 0-*

Each unit can be specified in one of the following ways:

* A single value, such as `30` for seconds or `12` for hours.
* A range of values, specified using a hyphen (`-`). For example, `15-20` represents the range of seconds from 15 to 20.
* A specific set of values, specified using a comma (`,`). For example, `5,10,15,20` represents the set of seconds {5, 10, 15, 20}.
* An interval unit, specified using a slash (`/`). For example, `/5` represents every 5 seconds.

Here are some examples of TimeBlock specifications:

|Spec|Explanation|
|----|-----------|
|\* * * * * *:| Matches all times.|
|/5 * * * * *:| Matches every 5 seconds.|
|* 20 10 Sun Dec 1995|Matches the specific date and time of December 5, 1995 at 10:20 AM.
|30-45 * * Mon,Tue,Fri * *|Matches all times between 30 and 45 seconds past the minute on Mondays, Tuesdays, and Fridays.|
