 $(function () {
        $('#wrapper .version strong').text('v' + $.fn.pignoseCalendar.version);

        function onSelectHandler(date, context) {
            /**
             * @date is an array which be included dates(clicked date at first index)
             * @context is an object which stored calendar interal data.
             * @context.calendar is a root element reference.
             * @context.calendar is a calendar element reference.
             * @context.storage.activeDates is all toggled data, If you use toggle type calendar.
             * @context.storage.events is all events associated to this date
             */

            var $element = context.element;
            var $calendar = context.calendar;
            var $box = $element.siblings('.box').show();
            var text = 'You selected date ';

            if (date[0] !== null) {
                text += date[0].format('YYYY-MM-DD');
            }

            if (date[0] !== null && date[1] !== null) {
                text += ' ~ ';
            }
            else if (date[0] === null && date[1] == null) {
                text += 'nothing';
            }

            if (date[1] !== null) {
                text += date[1].format('YYYY-MM-DD');
            }

            $box.text(text);
        }

        function onApplyHandler(date, context) {
          
            var $element = context.element;
            var $calendar = context.calendar;
            var $box = $element.siblings('.box').show();
            var text = 'You applied date ';

            if (date[0] !== null) {
                text += date[0].format('YYYY-MM-DD');
            }

            if (date[0] !== null && date[1] !== null) {
                text += ' ~ ';
            }
            else if (date[0] === null && date[1] == null) {
                text += 'nothing';
            }

            if (date[1] !== null) {
                text += date[1].format('YYYY-MM-DD');
            }

            $box.text(text);
        }

        // Default Calendar
        $('.calendar').pignoseCalendar({
            select: onSelectHandler
        });

        // Input Calendar
        $('.input-calendar').pignoseCalendar({
            apply: onApplyHandler,
            buttons: true, // It means you can give bottom button controller to the modal which be opened when you click.
        });

        // Calendar modal
        var $btn = $('.btn-calendar').pignoseCalendar({
            apply: onApplyHandler,
            modal: true, // It means modal will be showed when you click the target button.
            buttons: true
        });

        // Color theme type Calendar
        $('.calendar-dark').pignoseCalendar({
            theme: 'dark', // light, dark, blue
            select: onSelectHandler
        });

        // Blue theme type Calendar
        $('.calendar-blue').pignoseCalendar({
            theme: 'blue', // light, dark, blue
            select: onSelectHandler
        });

        // Multiple picker type Calendar
        $('.multi-select-calendar').pignoseCalendar({
            multiple: true,
            select: onSelectHandler
        });

        // Multiple Week Select
        $('.pick-weeks-calendar').pignoseCalendar({
            pickWeeks: true,
            multiple: true,
            select: onSelectHandler
        });

        // This use for DEMO page tab component.
        $('.menu .item').tab();
    });
    //]]>