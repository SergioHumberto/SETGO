<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistrarAdministrador.aspx.cs" Inherits="WebApplicationTemplate.Web.Pages.RegistrarAdministrador" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>

    <!-- Latest compiled and minified CSS -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous" />

    <!-- Latest compiled and minified JavaScript -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa" crossorigin="anonymous"></script>

    <div class="container" style="width: 80%">
        <div class="signup-form-container" style="width: 80%">
            <form role="form" id="register-form" autocomplete="off">

                <div class="form-header">
                    <h3 class="form-title"><i class="fa fa-user"></i> Registro</h3>      
                </div>

                <div class="form-body">
                    
                    <div class="form-group">
                        <div class="input-group">
                        <div class="input-group-addon"><span class="glyphicon glyphicon-user"></span></div>
                        <input name="username" type="text" class="form-control" placeholder="Nombre de usuario"/>
                        </div>
                        <span class="help-block" id="error"></span>
                        <label id="lblError" class="help-block"></label>
                    </div>

                    <div class="form-group">
                        <div class="input-group">
                        <div class="input-group-addon"><span class="glyphicon glyphicon-user"></span></div>
                        <input name="nombre" type="text" class="form-control" placeholder="Nombre(s)"/>
                        </div>
                        <span class="help-block" id="error"></span>
                    </div>

                    <div class="row">
                                            
                        <div class="form-group col-lg-6">
                            <div class="input-group">
                            <div class="input-group-addon"><span class="glyphicon glyphicon-user"></span></div>
                            <input name="paterno" id="paterno" class="form-control" placeholder="Apellido Paterno"/>
                            </div>  
                            <span class="help-block" id="error"></span>                    
                        </div>
                            
                        <div class="form-group col-lg-6">
                            <div class="input-group">
                            <div class="input-group-addon"><span class="glyphicon glyphicon-user"></span></div>
                            <input name="materno" class="form-control" placeholder="Apellido Materno">
                            </div>  
                            <span class="help-block" id="error"></span>                    
                        </div>
                            
                     </div>

                    <div class="form-group">
                        <div class="input-group">
                        <div class="input-group-addon"><span class="glyphicon glyphicon-envelope"></span></div>
                        <input name="email" type="text" class="form-control" placeholder="Email"/>
                        </div> 
                        <span class="help-block" id="error"></span>                     
                    </div>

                    <div class="row">
                                            
                        <div class="form-group col-lg-6">
                            <div class="input-group">
                            <div class="input-group-addon"><span class="glyphicon glyphicon-lock"></span></div>
                            <input name="password" id="password" type="password" class="form-control" placeholder="Contraseña" maxlength="15"/>
                            </div>  
                            <span class="help-block" id="error"></span>                    
                        </div>
                            
                        <div class="form-group col-lg-6">
                            <div class="input-group">
                            <div class="input-group-addon"><span class="glyphicon glyphicon-lock"></span></div>
                            <input name="cpassword" type="password" class="form-control" placeholder="Confirmar Contraseña" maxlength="15">
                            </div>  
                            <span class="help-block" id="error"></span>                    
                        </div>
                            
                     </div>

                </div>

                <div class="form-footer">
                    <button type="submit" class="btn btn-info" runat="server" onclick="prueba();">
                    <span class="glyphicon glyphicon-log-in"></span> Registrar
                    </button>
                </div>
                    
            </form>
        </div>
    </div>

    <script src="bootstrap/js/bootstrap.min.js"></script>
    <script src="assets/jquery-1.11.2.min.js"></script>
    <script src="assets/jquery.validate.min.js"></script>

    <script type="text/javascript">
        $('document').ready(function () {
            // name validation
            var nameregex = /^[a-zA-Z ]+$/;

            $.validator.addMethod("validname", function (value, element) {
                return this.optional(element) || nameregex.test(value);
            });

            // valid email pattern
            var eregex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;

            $.validator.addMethod("validemail", function (value, element) {
                return this.optional(element) || eregex.test(value);
            });

            $("#register-form").validate({

                rules:
                {
                    username: {
                        required: true,
                        minlength: 4
                    },
                    nombre: {
                        required: true,
                        validname: true,
                        minlength: 4
                    },
                    paterno: {
                        required: true,
                        validname: true,
                        minlength: 4
                    },
                    materno: {
                        required: true,
                        validname: true,
                        minlength: 4
                    },
                    email: {
                        required: true,
                        validemail: true
                    },
                    password: {
                        required: true,
                        minlength: 8,
                        maxlength: 15
                    },
                    cpassword: {
                        required: true,
                        minlength: 8,
                        maxlength: 15,
                        equalTo: '#password'
                    },
                },
                messages:
                {
                    username: {
                        required: "Por favor ingrese su nombre de usuario.",
                        minlength: "Su nombre de usuario es muy corto"
                    },
                    nombre: {
                        required: "Por favor ingrese su nombre.",
                        validname: "El nombre debe de contener solo letras.",
                        minlength: "Su nombre es muy corto."
                    },
                    paterno: {
                        required: "Por favor ingrese su apellido paterno.",
                        validname: "El apellido debe de contener solo letras.",
                        minlength: "El apellido es muy corto."
                    },
                    materno: {
                        required: "Por favor ingrese su apellido materno.",
                        validname: "El apellido debe de contener solo letras.",
                        minlength: "El apellido es muy corto."
                    },
                    email: {
                        required: "Por favor ingrese su dirección de correo electrónico.",
                        validemail: "Enter Valid Email Address"
                    },
                    password: {
                        required: "Por favor introdusca su contraseña.",
                        minlength: "La contraseña debe tener al menos 8 caracteres."
                    },
                    cpassword: {
                        required: "Por favor confirme su contraseña.",
                        equalTo: "La contraseña no coincide."
                    }
                },
                errorPlacement: function (error, element) {
                    $(element).closest('.form-group').find('.help-block').html(error.html());
                },
                highlight: function (element) {
                    $(element).closest('.form-group').removeClass('has-success').addClass('has-error');
                },
                unhighlight: function (element, errorClass, validClass) {
                    $(element).closest('.form-group').removeClass('has-error').addClass('has-success');
                    $(element).closest('.form-group').find('.help-block').html('');
                },

                submitHandler: function (form) {
                    form.submit();
                }
            });
        })
    </script>

</body>
</html>
