# SpecFlowProject

I've developed this project for the https://demo.1crmcloud.com/ site.

Below I've noted some difficulties/challenges in doing so, as well as potential improvements given more time.

## Difficulties/Challenges

I don't use C#/Selenium/SpecFlow day-to-day, so I found most of the difficulty being in the setup of the framework, and remembering how to best go about implementing certain things. 

Besides that, I didn't really have any issues. It went pretty smoothly, and I enjoyed the process.

I have implemented 1-2 implicit waits in certain areas around some 'janky' elements, that despite being clickable, when clicked would do nothing. There was definitely other options, but this was the easiesst without spending too much time on it.

I'm used to the auto-waiting and QOL features that Playwright offers, which I feel is a much more sophisticated choice in modern test automation. Selenium out of the box is not good, and while I could've implemented a number of methods to extend the web driver functionality, I felt as though that was going a little too far for this exercise.

## Potential Improvements

As aforementioned, I could've implemented a driver extension class, to extend some of the existing methods. I'd probably have put one in around clicking, so that it would automatically wait for the element to be clickable before trying to click it (therefore reducing all the _wait.untils everywhere)

I also didn't get around to logging in via the API. The request itself was fine, however the saving/restoring of cookies seemed a bit to time consuming for this exercise for me personally. I've implemented similar things using Playwright, however as I don't use Selenium/C# day-to-day, it would've taken me a decent chunk of time to figure it out.

## Takeaways

Overall, I had a lot of fun implementing this. I periodically use Selenium/C# to maintain a pre-existing test automation suite, so this was good training for that. Otherwise, the entire time I was just thinking to myself; "I'd be done by now if this was in Playwright".

Whether you take me on or not is your choice of course, but regardless, I think you should look into using Playwright over Selenium. Sure there's the overhead of migrating test automation scripts, but the QOL features it provides (i love auto-waiting) are unparalleled in delivery of modern day test automation.

Thanks
