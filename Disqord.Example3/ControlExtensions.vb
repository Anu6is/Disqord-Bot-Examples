Imports System.Linq.Expressions
Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports System.Windows.Forms

''' <summary>
'''     Thread-safe UI helper methods
''' </summary>
Public Module ControlExtensions
    ''' <summary>
    '''     Gets the value of the specified <see cref="Control"/> property
    ''' </summary>
    ''' <param name="control">The <see cref="Control"/> to which the property belongs</param>
    ''' <param name="expression">Lambda expression used to specify the property</param>
    <Extension()>
    Public Function GetProperty(Of TCtl As Control, TProp)(control As TCtl, expression As Expression(Of Func(Of TCtl, TProp))) As TProp
        If control Is Nothing Then Throw New ArgumentNullException(NameOf(control))
        If expression Is Nothing Then Throw New ArgumentNullException(NameOf(expression))

        If control.InvokeRequired Then
            Return CType(control.Invoke(New Func(Of TCtl, Expression(Of Func(Of TCtl, TProp)), TProp)(AddressOf GetProperty), control, expression), TProp)
        Else
            Dim memberExpr = TryCast(expression.Body, MemberExpression)
            If memberExpr Is Nothing Then Throw New ArgumentException("Invalid member expression.", NameOf(expression))

            Dim propertyInfo = TryCast(memberExpr.Member, PropertyInfo)
            If propertyInfo Is Nothing Then Throw New ArgumentException("Invalid property supplied.", NameOf(expression))

            Return CType(propertyInfo.GetValue(control), TProp)
        End If
    End Function

    ''' <summary>
    '''     Assigns a given value to a specified <see cref="Control"/> property
    ''' </summary>
    ''' <param name="control">The <see cref="Control"/> to which the property belongs</param>
    ''' <param name="expression">Lambda expression used to specify the property</param>
    ''' <param name="value">The value to be assigned to the property</param>
    <Extension()>
    Public Sub SetProperty(Of TCtl As Control, TProp)(control As TCtl, expression As Expression(Of Func(Of TCtl, TProp)), value As TProp)
        If control Is Nothing Then Throw New ArgumentNullException(NameOf(control))
        If expression Is Nothing Then Throw New ArgumentNullException(NameOf(expression))

        If control.InvokeRequired Then
            control.Invoke(New Action(Of TCtl, Expression(Of Func(Of TCtl, TProp)), TProp)(AddressOf SetProperty), New Object() {control, expression, value})
        Else
            Dim memberExpr = TryCast(expression.Body, MemberExpression)
            If memberExpr Is Nothing Then Throw New ArgumentException("Invalid member expression.", NameOf(expression))

            Dim propertyInfo = TryCast(memberExpr.Member, PropertyInfo)
            If propertyInfo Is Nothing Then Throw New ArgumentException("Invalid property supplied.", NameOf(expression))

            propertyInfo.SetValue(control, value)
        End If
    End Sub

    ''' <summary>
    '''     Invokes a method for the specified <see cref="Control"/>
    ''' </summary>
    ''' <param name="control">The <see cref="Control"/> that should execute the method</param>
    ''' <param name="action">The method (<see cref="Delegate"/>) to be executed</param>
    <Extension()>
    Public Sub InvokeAction(Of TCtl As Control)(control As TCtl, action As [Delegate])
        If control Is Nothing Then Throw New ArgumentNullException(NameOf(control))
        If action Is Nothing Then Throw New ArgumentNullException(NameOf(action))

        If control.InvokeRequired Then
            control.Invoke(New Action(Of TCtl, [Delegate])(AddressOf InvokeAction), control, action)
        Else
            action.DynamicInvoke()
        End If
    End Sub

    ''' <summary>
    '''     Invokes a function for the specified <see cref="Control"/>
    ''' </summary>
    ''' <param name="control">The <see cref="Control"/> that should execute the function</param>
    ''' <param name="func">the function (<see cref="Delegate"/>) to be executed</param>
    <Extension()>
    Public Function InvokeFunc(Of TCtl As Control, TResult)(control As TCtl, func As [Delegate]) As TResult
        If control Is Nothing Then Throw New ArgumentNullException(NameOf(control))
        If func Is Nothing Then Throw New ArgumentNullException(NameOf(func))

        If control.InvokeRequired Then
            Return CType(control.Invoke(New Func(Of TCtl, [Delegate], TResult)(AddressOf InvokeFunc), control, func), TResult)
        End If

        Return CType(func.DynamicInvoke(), TResult)
    End Function
End Module
