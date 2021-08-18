ko.bindingHandlers.Dropzone = {
    init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        var previewTemp = '<div class="dz-preview dz-file-preview"> <div class="dz-image"><img data-dz-thumbnail /></div> <div class="dz-details"> <div class="dz-size"><span data-dz-size></span></div> <div class="dz-filename"><span data-dz-name></span></div> </div> <div class="dz-progress"><span class="dz-upload" data-dz-uploadprogress></span></div> <div class="dz-error-message"><span data-dz-errormessage></span></div> <div class="dz-success-mark"> <svg width="54px" height="54px" viewBox="0 0 54 54" version="1.1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:sketch="http://www.bohemiancoding.com/sketch/ns"> <!-- Generator: Sketch 3.2.1 (9971) - http://www.bohemiancoding.com/sketch --> <title>Check</title> <desc>Created with Sketch.</desc> <defs></defs> <g id="Page-1" stroke="none" stroke-width="1" fill="none" fill-rule="evenodd" sketch:type="MSPage"> <path d="M23.5,31.8431458 L17.5852419,25.9283877 C16.0248253,24.3679711 13.4910294,24.366835 11.9289322,25.9289322 C10.3700136,27.4878508 10.3665912,30.0234455 11.9283877,31.5852419 L20.4147581,40.0716123 C20.5133999,40.1702541 20.6159315,40.2626649 20.7218615,40.3488435 C22.2835669,41.8725651 24.794234,41.8626202 26.3461564,40.3106978 L43.3106978,23.3461564 C44.8771021,21.7797521 44.8758057,19.2483887 43.3137085,17.6862915 C41.7547899,16.1273729 39.2176035,16.1255422 37.6538436,17.6893022 L23.5,31.8431458 Z M27,53 C41.3594035,53 53,41.3594035 53,27 C53,12.6405965 41.3594035,1 27,1 C12.6405965,1 1,12.6405965 1,27 C1,41.3594035 12.6405965,53 27,53 Z" id="Oval-2" stroke-opacity="0.198794158" stroke="#747474" fill-opacity="0.816519475" fill="#FFFFFF" sketch:type="MSShapeGroup"></path> </g> </svg> </div> <div class="dz-error-mark"> <svg width="54px" height="54px" viewBox="0 0 54 54" version="1.1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:sketch="http://www.bohemiancoding.com/sketch/ns"> <!-- Generator: Sketch 3.2.1 (9971) - http://www.bohemiancoding.com/sketch --> <title>error</title> <desc>Created with Sketch.</desc> <defs></defs> <g id="Page-1" stroke="none" stroke-width="1" fill="none" fill-rule="evenodd" sketch:type="MSPage"> <g id="Check-+-Oval-2" sketch:type="MSLayerGroup" stroke="#747474" stroke-opacity="0.198794158" fill="#FFFFFF" fill-opacity="0.816519475"> <path d="M32.6568542,29 L38.3106978,23.3461564 C39.8771021,21.7797521 39.8758057,19.2483887 38.3137085,17.6862915 C36.7547899,16.1273729 34.2176035,16.1255422 32.6538436,17.6893022 L27,23.3431458 L21.3461564,17.6893022 C19.7823965,16.1255422 17.2452101,16.1273729 15.6862915,17.6862915 C14.1241943,19.2483887 14.1228979,21.7797521 15.6893022,23.3461564 L21.3431458,29 L15.6893022,34.6538436 C14.1228979,36.2202479 14.1241943,38.7516113 15.6862915,40.3137085 C17.2452101,41.8726271 19.7823965,41.8744578 21.3461564,40.3106978 L27,34.6568542 L32.6538436,40.3106978 C34.2176035,41.8744578 36.7547899,41.8726271 38.3137085,40.3137085 C39.8758057,38.7516113 39.8771021,36.2202479 38.3106978,34.6538436 L32.6568542,29 Z M27,53 C41.3594035,53 53,41.3594035 53,27 C53,12.6405965 41.3594035,1 27,1 C12.6405965,1 1,12.6405965 1,27 C1,41.3594035 12.6405965,53 27,53 Z" id="Oval-2" sketch:type="MSShapeGroup"></path> </g> </g> </svg> </div> </div>';

        //console.log(valueAccessor().Upload());
        //console.log(valueAccessor().Remove());
        var dropzone = new Dropzone(element, {
            previewTemplate: previewTemp,
            url: valueAccessor().Upload(),
            parallelUploads: 2,
            thumbnailHeight: 120,
            thumbnailWidth: 120,
            maxFilesize: 5,
            acceptedFiles: "image/*,application/pdf",
            maxFiles: 20,
            filesizeBase: 1024,
            thumbnail: function (file, dataUrl) {
                if (file.previewElement) {
                    file.previewElement.classList.remove("dz-file-preview");
                    var images = file.previewElement.querySelectorAll("[data-dz-thumbnail]");
                    for (var i = 0; i < images.length; i++) {
                        var thumbnailElement = images[i];
                        thumbnailElement.alt = file.name;
                        thumbnailElement.src = dataUrl;
                    }
                    setTimeout(function () { file.previewElement.classList.add("dz-image-preview"); }, 1);
                }
            },
            init: function () {
                this.on("addedfile", function (file) {   
                    var removeButton = Dropzone.createElement("<a style='cursor:pointer; text-align: center; display: block;' href=\"#\">Eliminar</a>");
                    var _this = this;
                    removeButton.addEventListener("click", function (e) {
                        e.preventDefault();
                        e.stopPropagation();
                        _this.removeFile(file); 
                    });
                    file.previewElement.appendChild(removeButton);
                });
                this.on("error", function (file, errorMessage) {
                    file.previewTemplate.querySelector(".dz-error-message").setAttribute("style", "margin-top:" + 20 + "px");
                    file.previewTemplate.removeChild(file.previewTemplate.querySelector(".dz-progress"));
                });
                this.on("success", function (file, response) {
                    valueAccessor().Files.push(file);
                    file.previewTemplate.removeChild(file.previewTemplate.querySelector('.dz-progress'));
                    
                });
                this.on("removedfile", function (file) {
                    if (file.xhr) {
                        var id = JSON.parse(file.xhr.response).FileId;
                        $.ajax({
                            type: 'POST',
                            url: valueAccessor().Remove(),
                            data: { id: id },
                            dataType: 'json'
                        });
                        valueAccessor().Files.remove(file);
                    }
                });
            }
        });
    },
    update: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
     
    }
};


ko.bindingHandlers.DropzoneVideo = {
    init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        var previewTemp = '<div class="dz-preview dz-file-preview"> <div class="dz-image"><img data-dz-thumbnail /></div> <div class="dz-details"> <div class="dz-size"><span data-dz-size></span></div> <div class="dz-filename"><span data-dz-name></span></div> </div> <div class="dz-progress"><span class="dz-upload" data-dz-uploadprogress></span></div> <div class="dz-error-message"><span data-dz-errormessage></span></div> <div class="dz-success-mark"> <svg width="54px" height="54px" viewBox="0 0 54 54" version="1.1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:sketch="http://www.bohemiancoding.com/sketch/ns"> <!-- Generator: Sketch 3.2.1 (9971) - http://www.bohemiancoding.com/sketch --> <title>Check</title> <desc>Created with Sketch.</desc> <defs></defs> <g id="Page-1" stroke="none" stroke-width="1" fill="none" fill-rule="evenodd" sketch:type="MSPage"> <path d="M23.5,31.8431458 L17.5852419,25.9283877 C16.0248253,24.3679711 13.4910294,24.366835 11.9289322,25.9289322 C10.3700136,27.4878508 10.3665912,30.0234455 11.9283877,31.5852419 L20.4147581,40.0716123 C20.5133999,40.1702541 20.6159315,40.2626649 20.7218615,40.3488435 C22.2835669,41.8725651 24.794234,41.8626202 26.3461564,40.3106978 L43.3106978,23.3461564 C44.8771021,21.7797521 44.8758057,19.2483887 43.3137085,17.6862915 C41.7547899,16.1273729 39.2176035,16.1255422 37.6538436,17.6893022 L23.5,31.8431458 Z M27,53 C41.3594035,53 53,41.3594035 53,27 C53,12.6405965 41.3594035,1 27,1 C12.6405965,1 1,12.6405965 1,27 C1,41.3594035 12.6405965,53 27,53 Z" id="Oval-2" stroke-opacity="0.198794158" stroke="#747474" fill-opacity="0.816519475" fill="#FFFFFF" sketch:type="MSShapeGroup"></path> </g> </svg> </div> <div class="dz-error-mark"> <svg width="54px" height="54px" viewBox="0 0 54 54" version="1.1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:sketch="http://www.bohemiancoding.com/sketch/ns"> <!-- Generator: Sketch 3.2.1 (9971) - http://www.bohemiancoding.com/sketch --> <title>error</title> <desc>Created with Sketch.</desc> <defs></defs> <g id="Page-1" stroke="none" stroke-width="1" fill="none" fill-rule="evenodd" sketch:type="MSPage"> <g id="Check-+-Oval-2" sketch:type="MSLayerGroup" stroke="#747474" stroke-opacity="0.198794158" fill="#FFFFFF" fill-opacity="0.816519475"> <path d="M32.6568542,29 L38.3106978,23.3461564 C39.8771021,21.7797521 39.8758057,19.2483887 38.3137085,17.6862915 C36.7547899,16.1273729 34.2176035,16.1255422 32.6538436,17.6893022 L27,23.3431458 L21.3461564,17.6893022 C19.7823965,16.1255422 17.2452101,16.1273729 15.6862915,17.6862915 C14.1241943,19.2483887 14.1228979,21.7797521 15.6893022,23.3461564 L21.3431458,29 L15.6893022,34.6538436 C14.1228979,36.2202479 14.1241943,38.7516113 15.6862915,40.3137085 C17.2452101,41.8726271 19.7823965,41.8744578 21.3461564,40.3106978 L27,34.6568542 L32.6538436,40.3106978 C34.2176035,41.8744578 36.7547899,41.8726271 38.3137085,40.3137085 C39.8758057,38.7516113 39.8771021,36.2202479 38.3106978,34.6538436 L32.6568542,29 Z M27,53 C41.3594035,53 53,41.3594035 53,27 C53,12.6405965 41.3594035,1 27,1 C12.6405965,1 1,12.6405965 1,27 C1,41.3594035 12.6405965,53 27,53 Z" id="Oval-2" sketch:type="MSShapeGroup"></path> </g> </g> </svg> </div> </div>';

        //console.log(valueAccessor().Upload());
        //console.log(valueAccessor().Remove());
        var dropzone = new Dropzone(element, {
            previewTemplate: previewTemp,
            url: valueAccessor().Upload(),
            parallelUploads: 2,
            thumbnailHeight: 120,
            thumbnailWidth: 120,
            maxFilesize: 10,
            acceptedFiles: "video/*",
            maxFiles: 1,
            filesizeBase: 1024,
            thumbnail: function (file, dataUrl) {
                if (file.previewElement) {
                    file.previewElement.classList.remove("dz-file-preview");
                    var images = file.previewElement.querySelectorAll("[data-dz-thumbnail]");
                    for (var i = 0; i < images.length; i++) {
                        var thumbnailElement = images[i];
                        thumbnailElement.alt = file.name;
                        thumbnailElement.src = dataUrl;
                    }
                    setTimeout(function () { file.previewElement.classList.add("dz-image-preview"); }, 1);
                }
            },
            init: function () {
                this.on("addedfile", function (file) {
                    var removeButton = Dropzone.createElement("<a style='cursor:pointer; text-align: center; display: block;' href=\"#\">Eliminar</a>");
                    var _this = this;
                    removeButton.addEventListener("click", function (e) {
                        e.preventDefault();
                        e.stopPropagation();
                        _this.removeFile(file);
                    });
                    file.previewElement.appendChild(removeButton);
                });
                this.on("error", function (file, errorMessage) {
                    file.previewTemplate.querySelector(".dz-error-message").setAttribute("style", "margin-top:" + 20 + "px");
                    file.previewTemplate.removeChild(file.previewTemplate.querySelector(".dz-progress"));
                });
                this.on("success", function (file, response) {
                    valueAccessor().Files.push(file);
                    file.previewTemplate.removeChild(file.previewTemplate.querySelector('.dz-progress'));

                });
                this.on("removedfile", function (file) {
                    if (file.xhr) {
                        var id = JSON.parse(file.xhr.response).FileId;
                        $.ajax({
                            type: 'POST',
                            url: valueAccessor().Remove(),
                            data: { id: id },
                            dataType: 'json'
                        });
                        valueAccessor().Files.remove(file);
                    }
                });
            }
        });
    },
    update: function (element, valueAccessor, allBindings, viewModel, bindingContext) {

    }
};

ko.extenders.asyncComputed = function (evaluator, owner) {
    var result = ko.observable(), currentDeferred;
    result.inProgress = ko.observable(false); // Track whether we're waiting for a result

    ko.computed(function () {
        // Abort any in-flight evaluation to ensure we only notify with the latest value
        if (currentDeferred) { currentDeferred.reject(); }

        var evaluatorResult = evaluator.call(owner);
        // Cope with both asynchronous and synchronous values
        if (evaluatorResult && (typeof evaluatorResult.done == "function")) { // Async
            result.inProgress(true);
            currentDeferred = $.Deferred().done(function (data) {
                result.inProgress(false);
                result(data);
            });
            evaluatorResult.done(currentDeferred.resolve);
        } else // Sync
            result(evaluatorResult);
    });

    return result;
}

//CHOSEN
ko.bindingHandlers.chosenCie = {
    init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        var $element = $(element);
        var options = ko.unwrap(valueAccessor());

        //console.log(options);
        if (typeof options === 'object')
            $element.chosen(options);

        ['options', 'selectedOptions'].forEach(function (propName) {

            if (allBindings.has(propName)) {
                var prop = allBindings.get(propName);
                if (ko.isObservable(prop)) {
                    prop.subscribe(function () {
                        $element.trigger('chosen:updated');
                    });
                }
            }
        });
    },
    update: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        var $element = $(element);
        var options = ko.unwrap(valueAccessor());

        //console.log(options);
        if (typeof options === 'object')
            $element.chosen(options);

        ['options', 'selectedOptions'].forEach(function (propName) {

            if (allBindings.has(propName)) {
                var prop = allBindings.get(propName);
                if (ko.isObservable(prop)) {
                    prop.subscribe(function () {
                        $element.trigger('chosen:updated');
                    });
                }
            }
        });
    }
};

ko.bindingHandlers.chosen = {
    init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        var $element = $(element);
        var options = ko.unwrap(valueAccessor());

        if (typeof options === 'object') {
            $element.chosen(options);
        }
        else
            $element.chosen();

        ['options', 'selectedOptions', 'value'].forEach(function (propName) {
            if (allBindings.has(propName)) {
                var prop = allBindings.get(propName);
                if (ko.isObservable(prop)) {
                    prop.subscribe(function () {
                        $element.trigger('chosen:updated');
                    });
                }
            }
        });
    },
    update: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        var $element = $(element);
        var options = ko.unwrap(valueAccessor());

        if (typeof options === 'object') {
            $element.chosen(options);
        }
        else
            $element.chosen();

        ['options', 'selectedOptions', 'value'].forEach(function (propName) {
            if (allBindings.has(propName)) {
                var prop = allBindings.get(propName);
                if (ko.isObservable(prop)) {
                    prop.subscribe(function () {
                        $element.trigger('chosen:updated');
                    });
                }
            }
        });
    }

}


if (!ko.bindingHandlers.CKEDITOR) {
    ko.bindingHandlers.CKEDITOR = {
    init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        var ckEditorValue = valueAccessor();
        var id = $(element).attr('id');
        var options = allBindings().EditorOptions;
        var ignoreChanges = false;

        var instance = CKEDITOR.replace(id, {
            removeButtons: 'Source,About',
            width: '210mm',
            height: 400,
            on: {
                change: function () {
                    
                    ignoreChanges = true;
                    //console.log(instance);
                    ckEditorValue(instance.getData());
                    
                    ignoreChanges = false;
                }
            }
        });

        //if (ckEditorValue != undefined) {
            //ckEditorValue.subscribe(function (newValue) {
            //    if (!ignoreChanges) {
            //        instance.setData(newValue);
            //    }
            //});
        //}

    }
};
}


