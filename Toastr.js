<html>
  <body>
    <a id='linkButton'>ClickMe</a>
      
    <script src="http://code.jquery.com/jquery-1.9.1.min.js"></script>
      
    <link href="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/2.0.1/css/toastr.css" rel="stylesheet"/>
      
    <script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/2.0.1/js/toastr.js"></script>
      <script>
	  	toastr.options = {
  "closeButton": true,
  "debug": false,
  "newestOnTop": false,
  "progressBar": true,
  "positionClass": "toast-top-right",
  "preventDuplicates": false,
  "onclick": null,
  "showDuration": "300",
  "hideDuration": "1000",
  "timeOut": "5000",
  "extendedTimeOut": "1000",
  "showEasing": "swing",
  "hideEasing": "linear",
  "showMethod": "fadeIn",
  "hideMethod": "fadeOut"
}
	  </script>
    <script type="text/javascript">
      $(document).ready(function() {
        demo();
      });
	  function demo(){toastr.success('Click Button');}
	  
    </script>
  </body>
</html>
