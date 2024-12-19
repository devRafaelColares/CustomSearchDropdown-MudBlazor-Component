# CustomSearchDropdown Component

Um componente Blazor reutilizável que implementa um dropdown de pesquisa e seleção múltipla com funcionalidades avançadas.

## Demonstração do Projeto

[![Demonstração no YouTube](https://youtu.be/4eIkI5Xt7zI)]

## Características Principais

- Suporte a tipos genéricos (pode ser usado com qualquer tipo de dados)
- Pesquisa em tempo real
- Seleção múltipla
- Opção "Selecionar Todos"
- Indicadores visuais de seleção
- Design responsivo
- Customização através de parâmetros

## Parâmetros

```csharp
[Parameter] public string Label { get; set; }              // Rótulo do campo
[Parameter] public string PluralLabel { get; set; }        // Versão plural do rótulo
[Parameter] public string DropdownId { get; set; }         // ID único do dropdown
[Parameter] public List<T> Items { get; set; }             // Lista de itens
[Parameter] public List<T> SelectedItems { get; set; }     // Lista de itens selecionados
[Parameter] public RenderFragment<T> ItemTemplate { get; set; } // Template para renderização dos itens
```

## Uso Básico

```razor
<CustomSearchDropdown T="string"
    Label="Empresa"
    PluralLabel="empresas"
    DropdownId="companyDropdown"
    Items="@Companies"
    @bind-SelectedItems="SelectedCompanies">
    <ItemTemplate>@context</ItemTemplate>
</CustomSearchDropdown>
```

## Funcionalidades

### 1. Pesquisa
- Campo de busca com filtragem em tempo real
- Destaque visual dos itens que correspondem à pesquisa

### 2. Seleção
- Seleção individual de itens
- Opção "Selecionar Todos"
- Indicador visual do número de itens selecionados
- Checkbox visual para cada item

### 3. Interface
- Dropdown flutuante que não afeta o layout
- Scrolling automático para listas longas
- Ícones intuitivos para ações
- Tooltips informativos

### 4. Responsividade
- Adapta-se a diferentes tamanhos de tela
- Layout responsivo com grid system do MudBlazor

## Casos de Uso

### Filtros de Relatório

```razor
<CustomSearchDropdown T="string"
    Label="Departamento"
    PluralLabel="departamentos"
    Items="@Departments"
    @bind-SelectedItems="SelectedDepartments" />
```

### Seleção de Categorias

```razor
<CustomSearchDropdown T="Category"
    Label="Categoria"
    PluralLabel="categorias"
    Items="@Categories"
    @bind-SelectedItems="SelectedCategories">
    <ItemTemplate>@context.Name</ItemTemplate>
</CustomSearchDropdown>
```

### Multi-seleção de Usuários

```razor
<CustomSearchDropdown T="User"
    Label="Usuário"
    PluralLabel="usuários"
    Items="@Users"
    @bind-SelectedItems="SelectedUsers">
    <ItemTemplate>@context.FullName</ItemTemplate>
</CustomSearchDropdown>
```

## Estilização

O componente utiliza o sistema de design do MudBlazor e inclui estilos personalizados para:

- Dropdown flutuante
- Lista de itens
- Ícones e botões
- Estados hover e selecionado
- Responsividade

## Eventos e Callbacks

- SelectedItemsChanged: Notifica quando a seleção é alterada
- OnAfterRender: Configura listeners para cliques fora do dropdown
- CloseDropdown: Método invocável via JS para fechar o dropdown

## Benefícios

- Reutilização: Componente genérico que pode ser usado com qualquer tipo de dados
- Manutenabilidade: Código organizado e bem estruturado
- Experiência do Usuário: Interface intuitiva e responsiva
- Personalização: Fácil de customizar através de parâmetros e templates
- Integração: Funciona perfeitamente com o MudBlazor

## Dependências

- MudBlazor
- JavaScript Interop para detecção de cliques fora do dropdown
