/**
 * Adminre - Core: global.js
 * http://theme.pampersdry.info/adminre/html
 *
 * Copyright 2014-2015 John Pozy
 * Licensed: http://themeforest.net/licenses
 */
(function(factory) {

  'use strict';

  if (typeof define === 'function' && define.amd) {
    /** AMD. Register as an anonymous module. */
    define([
      'fastClick',
      'unveil',
      'bootstrap',
      'bootstrapHoverDropdown'
    ], factory);
  } else if (typeof exports === 'object') {
    /** Node/CommonJS */
    module.exports = factory(
      require('fastClick'),
      require('unveil'),
      require('bootstrap'),
      require('bootstrapHoverDropdown')
    );
  } else {
    /** Browser globals */
    factory();
  }
}(function() {

  'use strict';

  /**
   * CREATE GLOBAL VARIABLE
   */
  window.adminre = {
    eventPrefix: 'fa',
    isMinimize: false,
    isScreenlg: false,
    isScreenmd: false,
    isScreensm: false,
    isScreenxs: false,
    breakpoint: {
      'lg': 1200,
      'md': 992,
      'sm': 768,
      'xs': 480
    }
  };

  /**
   * VIEWPORT WATCHER
   */
  Response.action(function() {
    window.adminre.isScreenlg = Response.band(window.adminre.breakpoint.lg + 1);
    window.adminre.isScreenmd = Response.band(window.adminre.breakpoint.md + 1, window.adminre.breakpoint.lg);
    window.adminre.isScreensm = Response.band(window.adminre.breakpoint.sm + 1, window.adminre.breakpoint.md);
    window.adminre.isScreenxs = Response.band(0, window.adminre.breakpoint.sm);

    if (window.adminre.isScreenlg) {
      $('html')
        .addClass('screen-lg')
        .removeClass('screen-md')
        .removeClass('screen-sm')
        .removeClass('screen-xs');
    }

    if (window.adminre.isScreenmd) {
      $('html')
        .removeClass('screen-lg')
        .addClass('screen-md')
        .removeClass('screen-sm')
        .removeClass('screen-xs');
    }

    if (window.adminre.isScreensm) {
      $('html')
        .removeClass('screen-lg')
        .removeClass('screen-md')
        .addClass('screen-sm')
        .removeClass('screen-xs');
    }

    if (window.adminre.isScreenxs) {
      $('html')
        .removeClass('screen-lg')
        .removeClass('screen-md')
        .removeClass('screen-sm')
        .addClass('screen-xs');
    }
  });

  /**
   * INIT VARIOUS PLUGINS ON DOM READY
   */
  (function() {
    /** fastclick INIT */
    FastClick.attach(document.body);

    /** unveil INIT */
    $('[data-toggle~=unveil]').unveil(200, function() {
      $(this).load(function() {
        $(this).addClass('unveiled');
      });
    });

    /** Bootstrap tooltip INIT */
    $('[data-toggle~=tooltip]').tooltip();

    /** Bootstrap popover INIT */
    $('[data-toggle~=popover]').popover();

    /** Bootstrap dropdown hover INIT */
    $('[data-toggle="dropdown"].dropdown-hover').dropdownHover().dropdown();

    /** IE9 input placeholder INIT */
    $('input, textarea').placeholder();
  })();

}));

/**
 * Adminre - Core: scrollbar.js
 * http://theme.pampersdry.info/adminre/html
 *
 * Copyright 2014-2015 John Pozy
 * Licensed: http://themeforest.net/licenses
 */
(function(factory) {

  'use strict';

  if (typeof define === 'function' && define.amd) {
    /** AMD. Register as an anonymous module. */
    define([
      'slimScroll'
    ], factory);
  } else if (typeof exports === 'object') {
    /** Node/CommonJS */
    module.exports = factory(
      require('slimScroll')
    );
  } else {
    /** Browser globals */
    factory();
  }
}(function() {

  'use strict';

  /**
   * CREATE DEFAULTS ONCE
   */
  $('.no-touch .slimscroll').each(function(index, value) {
    $(value).slimScroll({
      size: '8px',
      height: false,
      distance: '0px',
      wrapperClass: $(value).data('wrapper') || 'viewport',
      railClass: 'scrollrail',
      barClass: 'scrollbar',
      wheelStep: 10,
      railVisible: false
    });
  });

}));

/**
 * Adminre - Core: totop.js
 * http://theme.pampersdry.info/adminre/html
 *
 * Copyright 2014-2015 John Pozy
 * Licensed: http://themeforest.net/licenses
 */
(function(factory) {

  'use strict';

  if (typeof define === 'function' && define.amd) {
    /** AMD. Register as an anonymous module. */
    define([
      'velocity'
    ], factory);
  } else if (typeof exports === 'object') {
    /** Node/CommonJS */
    module.exports = factory(
      require('velocity')
    );
  } else {
    /** Browser globals */
    factory();
  }
}(function() {

  'use strict';

  /**
   * CREATE DEFAULTS ONCE
   */
  var pluginName = 'totop';

  /**
   * PLUGIN CONSTRUCTOR
   */
  var Plugin = function(elem, options) {
    this.elem = elem;
    this.$elem = $(elem);
    this.options = options;
  };

  /**
   * PLUGIN PROTOTYPE
   */
  Plugin.prototype = {
    defaults: {
      easing: [0.165, 0.84, 0.44, 1],
      duration: 500,
      delay: 0
    },

    init: function() {
      this.config = $.extend({}, this.defaults, this.options);

      var totop = this;

      totop.$elem.velocity('scroll', {
        easing: totop.config.easing,
        duration: totop.config.duration,
        delay: totop.config.delay
      });

      return totop;
    }
  };

  /**
   * PLUGIN DEFAULTS
   */
  Plugin.defaults = Plugin.prototype.defaults;

  /**
   * PLUGIN WRAPPER
   */
  $.fn.coreTotop = function(options) {
    return this.each(function() {
      $.data(this, pluginName, new Plugin(this, options).init());
    });
  };

  /**
   * PLUGIN DATA-API
   */
  $(document).on('click', '[data-toggle~="' + pluginName + '"]', function(e) {
    var target = $(this).data(pluginName + '-target') || 'html';
    var options = $(this).data(pluginName + '-options');

    $(target).coreTotop(options);

    e.preventDefault();
  });
}));

/**
 * Adminre - Core: waypoints.js
 * http://theme.pampersdry.info/adminre/html
 *
 * Copyright 2014-2015 John Pozy
 * Licensed: http://themeforest.net/licenses
 */
(function(factory) {

  'use strict';

  if (typeof define === 'function' && define.amd) {
    /** AMD. Register as an anonymous module. */
    define([
      'waypoints'
    ], factory);
  } else if (typeof exports === 'object') {
    /** Node/CommonJS */
    module.exports = factory(
      require('waypoints')
    );
  } else {
    /** Browser globals */
    factory();
  }
}(function() {

  'use strict';

  /**
   * CREATE DEFAULTS ONCE
   */
  var toggler = '[data-toggle~=waypoints]';

  /**
   * LOOP THROUGH EACH TOGGLER
   */
  $(toggler).each(function() {
    var wayShowAnimation = $(this).data('showanim') || 'fadeIn';
    var wayHideAnimation = $(this).data('hideanim') || false;
    var wayOffset = $(this).data('offset') || '80%';
    var wayMarker = $(this).data('marker') || this;

    /** waypoints core */
    $(wayMarker).waypoint({
      handler: function(direction) {
        if (direction === 'down') {
          $(wayMarker)
            .removeClass(wayHideAnimation + ' animated')
            .addClass(wayShowAnimation + ' animating')
            .on('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function() {
              $(this)
                .removeClass('animating')
                .addClass('animated')
                .removeClass(wayShowAnimation);
            });
        }
        if ((direction === 'up') && (wayHideAnimation !== false)) {
          $(wayMarker)
            .removeClass(wayShowAnimation + ' animated')
            .addClass(wayHideAnimation + ' animating')
            .on('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function() {
              $(this)
                .removeClass('animating')
                .removeClass('animated')
                .removeClass(wayHideAnimation);
            });
        }
      },
      continuous: true,
      offset: wayOffset
    });
  });

}));

/**
 * Adminre - Core: inview.js
 * http://theme.pampersdry.info/adminre/html
 *
 * Copyright 2014-2015 John Pozy
 * Licensed: http://themeforest.net/licenses
 */
(function(factory) {

  'use strict';

  if (typeof define === 'function' && define.amd) {
    /** AMD. Register as an anonymous module. */
    define([
      'waypoints'
    ], factory);
  } else if (typeof exports === 'object') {
    /** Node/CommonJS */
    module.exports = factory(
      require('waypoints')
    );
  } else {
    /** Browser globals */
    factory();
  }
}(function() {

  'use strict';

  /**
   * CREATE DEFAULT ONCE
   */
  var toggler = '[data-toggle~=inview]';

  /**
   * LOOP THROUGH EACH TOGGLER
   */
  $(toggler).each(function() {
    var inviewEnterClass = $(this).data('enter-class') || 'inview-enter';
    var inviewEnteredClass = $(this).data('entered-class') || 'inview-entered';
    var inviewExitClass = $(this).data('exit-class') || 'inview-exit';
    var inviewExitedClass = $(this).data('exited-class') || 'inview-exited';

    new Waypoint.Inview({
      element: this,
      enter: function() {
        $(this.element)
          .removeClass(inviewEnteredClass)
          .removeClass(inviewExitClass)
          .removeClass(inviewExitedClass)
          .addClass(inviewEnterClass);
      },
      entered: function() {
        $(this.element)
          .removeClass(inviewEnterClass)
          .removeClass(inviewExitClass)
          .removeClass(inviewExitedClass)
          .addClass(inviewEnteredClass);
      },
      exit: function() {
        $(this.element)
          .removeClass(inviewEnterClass)
          .removeClass(inviewEnteredClass)
          .removeClass(inviewExitedClass)
          .addClass(inviewExitClass);
      },
      exited: function() {
        $(this.element)
          .removeClass(inviewEnterClass)
          .removeClass(inviewEnteredClass)
          .removeClass(inviewExitClass)
          .addClass(inviewExitedClass);
      }
    });
  });

}));

/**
 * Adminre - Core: sidebar-minimizer.js
 * http://theme.pampersdry.info/adminre/html
 *
 * Copyright 2014-2015 John Pozy
 * Licensed: http://themeforest.net/licenses
 */
(function(factory) {

  'use strict';

  if (typeof define === 'function' && define.amd) {
    /** AMD. Register as an anonymous module. */
    define([], factory);
  } else if (typeof exports === 'object') {
    /** Node/CommonJS */
    module.exports = factory();
  } else {
    /** Browser globals */
    factory();
  }
}(function() {

  'use strict';

  /**
   * CREATE DEFAULT ONCE
   */
  var minimizeHandler = '[data-toggle~=minimize]';

  /**
   * CORE MINIMIZE FUNCTION
   */
  var toggleMinimize = function(e) {
    /** toggle class */
    if ($('html').hasClass('sidebar-minimized')) {
      window.adminre.isMinimize = false;
      Cookies.remove('menu');
      $('html').removeClass('sidebar-minimized');
      $('html').addClass('sidebar-maximized');
      $('aside.sidebar-menu').removeClass('hoverable-menu');
      /** publish event */
      $('html').trigger(window.adminre.eventPrefix + '.sidebar.maximize');
    } else {
      window.adminre.isMinimize = true;
      Cookies.set('menu', 'xs');
      $('html').removeClass('sidebar-maximized');
      $('html').addClass('sidebar-minimized');
      $('aside.sidebar-menu').addClass('hoverable-menu');
    	//hacer que se eliminen las clases  	
    	$('#menu_principal li ul.submenu').removeClass('in');  	
    	$('#menu_principal li ul.submenu li').removeClass('open');   	
      $('#menu_principal li ul.submenu li ul.submenu').removeClass('in');
      /** publish event */
      $('html').trigger(window.adminre.eventPrefix + '.sidebar.minimize');
    }

    /** prevent default */
    e.preventDefault();
  };

  /**
   * CLICK EVENT
   */
  $(document).on('click', minimizeHandler, toggleMinimize);
}));

/**
 * Adminre - Core: sidebar-menu.js
 * http://theme.pampersdry.info/adminre/html
 *
 * Copyright 2014-2015 John Pozy
 * Licensed: http://themeforest.net/licenses
 */
(function(factory) {

  'use strict';

  if (typeof define === 'function' && define.amd) {
    /** AMD. Register as an anonymous module. */
    define([], factory);
  } else if (typeof exports === 'object') {
    /** Node/CommonJS */
    module.exports = factory();
  } else {
    /** Browser globals */
    factory();
  }
}(function() {

  'use strict';

  /**
   * CREATE DEFAULTS ONCE
   */
  var menuHandler = '[data-toggle~=menu]';
  var submenuHandler = '[data-toggle~=submenu]';

  /**
   * HANDLE CLICK
   */
  var handleClick = function(e) {
    var $this = $(this);
    var parent = $this.data('parent');
    var target = $this.data('target');

    /** default click event handler */
    if (e.type === 'click') {
      /** toggle hide and show */
      if ($(target).hasClass('in')) {
        /** hide the submenu */
        $(target).collapse('hide');
        $this.parent().removeClass('open');
      } else {
        /** hide other showed target if parent is defined */
        if (!!parent) {
          $(parent + ' .in').each(function() {
            $(this).collapse('hide');
            $(this).parent().removeClass('open');
          });
        }

        /** show the submenu */
        $(target).collapse('show');
        $this.parent().addClass('open');
      }
    }

    /** run only on tablet view and sidebar-menu collapse */
    if ((window.adminre.isScreensm) || (window.adminre.isMinimize)) {
      /** if have target */
      if ($(target).length > 0) {
        /** touch devices */
        if ($('html').hasClass('touch')) {
          /** click event handler */
          if (e.type === 'click') {
            if ($this.parent().hasClass('hover')) {
              /** remove hover class and clear the `top` css attr val */
              $this.parent().removeClass('hover');
              $(target).css('top', '');
            } else {
              /** remove other opened submenus */
              if (parent) {
                $(parent + ' .hover').each(function(index, elem) {
                  $(elem).removeClass('hover');
                });
              }

              /** add hover class and calculate submenu offset */
              // $this.parent().addClass('hover');
              // if ($(target)[0].getBoundingClientRect().bottom >= Response.deviceH()) {
              //   $(target).css('top', '-' + ($(target)[0].getBoundingClientRect().bottom - Response.deviceH() + 2) + 'px');
              // }
            }
          }
        }
      }
    }
  };

  /**
   * HANDLE HOVER
   */
  var handleHover = function(e) {
  //   var $this = $(this);
  //   //var parent      = $this.children(submenuHandler).data('parent');
  //   var target = $this.children(submenuHandler).data('target');

  //   /** run only on tablet view and sidebar-menu collapse */
  //   if ((window.adminre.isScreensm) || (window.adminre.isMinimize)) {
  //     /** if have target */
  //     if ($(target).length > 0) {
  //       /** touch devices */
  //       if (!$('html').hasClass('touch')) {

  //         /** mouseenter event handler */
  //         if (e.type === 'mouseenter') {
  //           /** add hover class and calculate submenu offset */
  //           $this.addClass('hover open');
  //           if ($(target)[0].getBoundingClientRect().bottom >= Response.deviceH()) {
  //             $(target).css('top', '-' + ($(target)[0].getBoundingClientRect().bottom - Response.deviceH() + 2) + 'px');
  //           }
  //         }

  //         /** mouseleave event handler */
  //         if (e.type === 'mouseleave') {
  //           /** remove hover class and clear the `top` css attr val */
  //           $this.removeClass('hover open');
  //           $(target).css('top', '');
  //         }

  //       }
  //     }
  //   }
  };

  /**
   * ON DOM READY
   */
  $(document).on('click', submenuHandler, handleClick);
  $(document).on('mouseenter mouseleave', menuHandler + ' > li', handleHover);

  try {
    ejecutarMenu();
  }
  catch(err) {
      console.info('contifico orders');
  }
  

}));

/**
 * Adminre - Core: sidebar-toggle.js
 * http://theme.pampersdry.info/adminre/html
 *
 * Copyright 2014-2015 John Pozy
 * Licensed: http://themeforest.net/licenses
 */
(function(factory) {

  'use strict';

  if (typeof define === 'function' && define.amd) {
    /** AMD. Register as an anonymous module. */
    define([], factory);
  } else if (typeof exports === 'object') {
    /** Node/CommonJS */
    module.exports = factory();
  } else {
    /** Browser globals */
    factory();
  }
}(function() {

  'use strict';

  /**
   * CREATE DEFAULTS ONCE
   */
  var pluginName = 'sidebar-toggle';
  var openClass = 'sidebar-open';
  var sidebar = '';

  /**
   * PLUGIN CONSTRUCTOR
   */
  var Plugin = function(elem, options) {
    this.elem = elem;
    this.$elem = $(elem);
    this.options = options;
  };

  /**
   * PLUGIN PROTOTYPE
   */
  Plugin.prototype = {
    defaults: {
      direction: null
    },

    init: function() {
      this.config = $.extend({}, this.defaults, this.options);
      sidebar = this.config.direction === 'ltr' ? '.sidebar-left' : '.sidebar-right';

      if (this.config.direction === null) {
        $.error('missing `data-direction` value (ltr or rtl)');
      }

      return this;
    },

    toggle: function() {
      $('html').hasClass(openClass + '-' + this.config.direction) ? this.close() : this.open();
    },

    open: function() {
      $('html').addClass(openClass + '-' + this.config.direction);
      $('html').trigger(window.adminre.eventPrefix + '.sidebar.open', {
        'element': $(sidebar)
      });
    },

    close: function() {
      if ($('html').hasClass(openClass + '-' + this.config.direction)) {
        $('html').removeClass(openClass + '-' + this.config.direction);
        $('html').trigger(window.adminre.eventPrefix + '.sidebar.close', {
          'element': $(sidebar)
        });
      }
    }
  };

  /**
   * PLUGIN DEFAULTS
   */
  Plugin.defaults = Plugin.prototype.defaults;

  /**
   * PLUGIN WRAPPER
   */
  $.fn.coreSidebarToggle = function(options) {
    return this.each(function() {
      $.data(this, pluginName, new Plugin(this, options).init());
    });
  };

  /**
   * PLUGIN DATA-API
   */
  $(document).on('click', function() {
    if ($(sidebar).length > 0) {
      $('html').removeClass('sidebar-open-rtl sidebar-open-ltr');

      sidebar = '';
    }
  });
  $(document).on('click', '.sidebar, [data-toggle~="sidebar"]', function(e) {
    e.stopPropagation();
  });
  $(document).on('click', '[data-toggle~="sidebar"]', function(e) {
    var $this = $(this);
    var target = 'html';
    var options = $.extend({}, $this.data(pluginName + '-options'), {
      'direction': $this.data('direction')
    });

    $(target).coreSidebarToggle(options).data(pluginName).toggle();

    e.preventDefault();
  });
}));

/**
 * Adminre - Core: select-row.js
 * http://theme.pampersdry.info/adminre/html
 *
 * Copyright 2014-2015 John Pozy
 * Licensed: http://themeforest.net/licenses
 */
(function(factory) {

  'use strict';

  if (typeof define === 'function' && define.amd) {
    /** AMD. Register as an anonymous module. */
    define([], factory);
  } else if (typeof exports === 'object') {
    /** Node/CommonJS */
    module.exports = factory();
  } else {
    /** Browser globals */
    factory();
  }
}(function() {

  'use strict';

  /**
   * CREATE DEFAULT ONCE
   */
  var contextual;
  var toggler = '[data-toggle~=selectrow]';
  var target = $(toggler).data('target');

  /**
   * CORE SELECTROW FUNCTION
   * @state: checked/unchecked
   */
  var selectrow = function(row, state) {
    // contextual
    if ($(row).data('contextual')) {
      contextual = $(row).data('contextual');
    } else {
      contextual = 'active';
    }

    if (state === 'checked') {
      // add contextual class
      $(row).parents(target).addClass(contextual);

      // publish event
      $('html').trigger(window.adminre.eventPrefix + '.selectrow.selected', {
        'element': $(row).parents(target)
      });
    } else {
      // remove contextual class
      $(row).parents(target).removeClass(contextual);

      // publish event
      $('html').trigger(window.adminre.eventPrefix + '.selectrow.unselected', {
        'element': $(row).parents(target)
      });
    }
  };

  /**
   * LOOP THROUGH EACH TOGGLER
   */
  $(toggler).each(function() {
    if ($(this).is(':checked')) {
      selectrow(this, 'checked');
    }
  });

  /**
   * CLICK EVENT
   */
  $(document).on('change', toggler, function() {
    /** checked / unchecked */
    if ($(this).is(':checked')) {
      selectrow(this, 'checked');
    } else {
      selectrow(this, 'unchecked');
    }
  });

}));

/**
 * Adminre - Core: checkall.js
 * http://theme.pampersdry.info/adminre/html
 *
 * Copyright 2014-2015 John Pozy
 * Licensed: http://themeforest.net/licenses
 */
(function(factory) {

  'use strict';

  if (typeof define === 'function' && define.amd) {
    /** AMD. Register as an anonymous module. */
    define([], factory);
  } else if (typeof exports === 'object') {
    /** Node/CommonJS */
    module.exports = factory();
  } else {
    /** Browser globals */
    factory();
  }
}(function() {

  'use strict';

  /**
   * CREATE DEFAULTS ONCE
   */
  var toggler = '[data-toggle~=checkall]';

  /**
   * LOOP THROUGH EACH TOGGLER
   */
  $(toggler).each(function() {
    if ($(this).is(':checked')) {
      checked();
    }
  });

  /**
   * ON CLICK EVENT
   */
  $(document).on('change', toggler, function() {
    var target = $(this).data('target');

    // checked / unchecked
    if ($(this).is(':checked')) {
      checked(target);
    } else {
      unchecked(target);
    }
  });

  /**
   * CORE CHECKED FUNCTION
   */
  var checked = function(target) {
    // find checkbox
    $(target).find('input[type=checkbox]').each(function() {
      // select row
      if ($(this).data('toggle') === 'selectrow') {
        // trigger change event
        if (!$(this).is(':checked')) {
          $(this)
            .prop('checked', true)
            .trigger('change');
        }
      }
    });

    // publish event
    $('html').trigger(window.adminre.eventPrefix + '.checkall.checked', {
      'element': $(target)
    });
  };

  /**
   * CORE UNCHECKED FUNCTION
   */
  var unchecked = function(target) {
    // find checkbox
    $(target).find('input[type=checkbox]').each(function() {
      // select row
      if ($(this).data('toggle') === 'selectrow') {
        // trigger change event
        if ($(this).is(':checked')) {
          $(this)
            .prop('checked', false)
            .trigger('change');
        }
      }
    });

    // publish event
    $('html').trigger(window.adminre.eventPrefix + '.checkall.unchecked', {
      'element': $(target)
    });
  };

}));

/**
 * Adminre - Core: panel-collapse.js
 * http://theme.pampersdry.info/adminre/html
 *
 * Copyright 2014-2015 John Pozy
 * Licensed: http://themeforest.net/licenses
 */
(function(factory) {

  'use strict';

  if (typeof define === 'function' && define.amd) {
    /** AMD. Register as an anonymous module. */
    define([
      'velocity'
    ], factory);
  } else if (typeof exports === 'object') {
    /** Node/CommonJS */
    module.exports = factory(
      require('velocity')
    );
  } else {
    /** Browser globals */
    factory();
  }
}(function() {

  'use strict';

  /**
   * CREATE DEFAULT ONCE
   */
  var toggler = '[data-toggle~=panelcollapse]';

  /**
   * CLICK EVENT
   */
  $(document).on('click', toggler, function(e) {
    /** find panel element */
    var panel = $(this).parents('.panel');
    var target = panel.children('.panel-collapse');
    var height = target.innerHeight();

    /** error handling */
    if (target.length === 0) {
      $.error('collapsable element need to be wrap inside ".panel-collapse"');
    }

    /** core open function */
    var open = function(toggler) {
      $(toggler).removeClass('down').addClass('up');
      $(target)
        .removeClass('pull').addClass('pulling')
        .velocity({
          opacity: [1, 0],
          height: [height, 0]
        }, {
          display: 'block',
          easing: 'easeOutCubic',
          duration: 500,
          complete: function(elem) {
            $(elem)
              .removeClass('pulling').addClass('pull out')
              .removeAttr('style');
          }
        });

      // publish event
      $('html').trigger(window.adminre.eventPrefix + '.panelcollapse.open', {
        'element': $(panel)
      });
    };

    /** core close function */
    var close = function(toggler) {
      $(toggler).removeClass('up').addClass('down');
      $(target)
        .removeClass('pull out').addClass('pulling')
        .velocity({
          opacity: [0, 1],
          height: [0, height]
        }, {
          display: 'block',
          easing: 'easeOutCubic',
          duration: 500,
          complete: function(elem) {
            $(elem)
              .removeClass('pulling').addClass('pull')
              .removeAttr('style');
          }
        });

      // publish event
      $('html').trigger(window.adminre.eventPrefix + '.panelcollapse.close', {
        'element': $(panel)
      });
    };

    /** collapse the element */
    if ($(target).hasClass('out')) {
      close(this);
    } else {
      open(this);
    }

    /** prevent default */
    e.preventDefault();
  });

}));

/**
 * Adminre - Core: panel-refresh.js
 * http://theme.pampersdry.info/adminre/html
 *
 * Copyright 2014-2015 John Pozy
 * Licensed: http://themeforest.net/licenses
 */
(function(factory) {

  'use strict';

  if (typeof define === 'function' && define.amd) {
    /** AMD. Register as an anonymous module. */
    define([], factory);
  } else if (typeof exports === 'object') {
    /** Node/CommonJS */
    module.exports = factory();
  } else {
    /** Browser globals */
    factory();
  }
}(function() {

  'use strict';

  /**
   * CREATE DEFAULT ONCE
   */
  var isDemo = false;
  var indicatorClass = 'indicator';
  var toggler = '[data-toggle~=panelrefresh]';

  /**
   * CLICK EVENT
   */
  $(document).on('click', toggler, function(e) {
    /** find panel element */
    var panel = $(this).parents('.panel');
    var indicator = panel.find('.' + indicatorClass);

    /** check if demo or not */
    if ($(this).hasClass('demo')) {
      isDemo = true;
    } else {
      isDemo = false;
    }

    /** check indicator */
    if (indicator.length !== 0) {
      indicator.addClass('show');

      /** check if demo or not */
      if (isDemo) {
        setTimeout(function() {
          indicator.removeClass('show');
        }, 2000);
      }

      /** publish event */
      $('html').trigger(window.adminre.eventPrefix + '.panelrefresh.refresh', {
        'element': $(panel)
      });
    } else {
      $.error('There is no `indicator` element inside this panel.');
    }

    /** prevent default */
    e.preventDefault();
  });

}));

/**
 * Adminre - Core: panel-remove.js
 * http://theme.pampersdry.info/adminre/html
 *
 * Copyright 2014-2015 John Pozy
 * Licensed: http://themeforest.net/licenses
 */
(function(factory) {

  'use strict';

  if (typeof define === 'function' && define.amd) {
    /** AMD. Register as an anonymous module. */
    define([], factory);
  } else if (typeof exports === 'object') {
    /** Node/CommonJS */
    module.exports = factory();
  } else {
    /** Browser globals */
    factory();
  }
}(function() {

  'use strict';

  /**
   * CREATE DEFAULT ONCE
   */
  var panel;
  var parent;
  var handler = '[data-toggle~=panelremove]';

  /**
   * CLICK EVENT
   */
  $(document).on('click', handler, function(e) {
    /** find panel element */
    panel = $(this).parents('.panel');
    parent = $(this).data('parent');

    /** remove panel */
    panel.velocity({
      opacity: 0,
      scale: 0
    }, {
      easing: 'easeOutCubic',
      duration: 500,
      complete: function(elem) {
        //remove
        if (parent) {
          $(elem).parents(parent).remove();
        } else {
          $(elem).remove();
        }

        /** publish event */
        $('html').trigger(window.adminre.eventPrefix + '.panelcollapse.remove', {
          'element': $(panel)
        });
      }
    });

    /** prevent default */
    e.preventDefault();
  });

}));

/**
 * Adminre - Core: offcanvas.js
 * http://theme.pampersdry.info/adminre/html
 *
 * Copyright 2014-2015 John Pozy
 * Licensed: http://themeforest.net/licenses
 */

(function(factory) {

  'use strict';

  if (typeof define === 'function' && define.amd) {
    /** AMD. Register as an anonymous module. */
    define([], factory);
  } else if (typeof exports === 'object') {
    /** Node/CommonJS */
    module.exports = factory();
  } else {
    /** Browser globals */
    factory();
  }
}(function() {

  'use strict';

  /**
   * CREATE DEFAULTS ONCE
   */
  var container = '[data-toggle~=offcanvas]';
  var pluginErrors = [];
  var direction;
  var optOpenerClass;
  var optCloserClass;

  /**
   * LOOP THROUGH EACH CONTAINER
   */
  $(container).each(function(index, value) {
    /** define variable */
    var options = $(value).data('options');

    /** check for valid options object */
    if (options !== undefined) {
      if (typeof options !== 'object') {
        pluginErrors.push('OffCanvas: `data-options` need to be a valid javascript object!');
      } else {
        /** set value */
        optOpenerClass = 'offcanvas-opener' || options.openerClass;
        optCloserClass = 'offcanvas-closer' || options.closerClass;
      }
    } else {
      /** set default value */
      optOpenerClass = 'offcanvas-opener';
      optCloserClass = 'offcanvas-closer';
    }

    /** check for errors */
    if (pluginErrors.length <= 0) {
      $(value)
        .on('click', '.' + optOpenerClass, function(e) {
          /** get direction */
          if ($(this).hasClass('offcanvas-open-rtl')) {
            direction = 'offcanvas-open-rtl';
          } else {
            direction = 'offcanvas-open-ltr';
          }

          $(value)
            .removeClass('offcanvas-open-ltr offcanvas-open-rtl')
            .addClass(direction);

          /** trigger custom event */
          $('html').trigger(window.adminre.eventPrefix + '.offcanvas.open', {
            'element': $(value)
          });

          /** prevent default */
          e.preventDefault();
        }).on('click', '.' + optCloserClass, function(e) {
          $(value)
            .removeClass('offcanvas-open-ltr offcanvas-open-rtl');

          /** trigger custom event */
          $('html').trigger(window.adminre.eventPrefix + '.offcanvas.close', {
            'element': $(value)
          });

          /** prevent default */
          e.preventDefault();
        });
    } else {
      $.each(pluginErrors, function(index, value) {
        $.error(value);
      });
    }
  });

}));
