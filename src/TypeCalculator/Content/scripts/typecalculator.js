$(document).ready(function() {
  var setupTypeDropdown = function() {
        $.ajax('types/getTypes', {
          data: {},
          success: function(data) {
            var type;
            for (type in data.Types) {
              type = data.Types[type];
              $('#typeSelectOne').append('<option value=' + type.toLowerCase() + '>' + type + '</option>');
              $('#typeSelectTwo').append('<option value=' + type.toLowerCase() + '>' + type + '</option>');
            }
          }
        });
        updateValues('None', 'None');
      },
    updateStrongAttack = function(strongAttack) {
      update(strongAttack, 'strong', 'attack');
    },
    updateWeakAttack = function (weakAttack) {
      update(weakAttack, 'weak', 'attack');
    },
    updateWeakDefense = function(weakDefense) {
      update(weakDefense, 'weak', 'defense');
    },
    updateImmunities = function(immuneDefense) {
      update(immuneDefense, 'immune', 'defense');
    },
    updateStrongDefense = function(strongDefense) {
      update(strongDefense, 'strong', 'defense');
    },
    update = function(elementTypes, id, section) {
      var selector = '#' + section + 'Section .' + id + ' .types',
        i;

      $(selector).html('');

      if (!elementTypes || elementTypes.length === 0) {
        $(selector).append('<span class="type displayInline none">None</span>');
        return;
      }

      for (i = 0; i < elementTypes.length; i++) {
        $(selector).append('<span class="type displayInline ' + elementTypes[i].toLowerCase() + '">' + elementTypes[i] + '</span>');
      }
    },
    updateValues = function(type, typeTwo) {
      changeStatus('Updating');
      $.ajax('types/getStats', {
        data: { ElementOne: type, ElementTwo: typeTwo },
        success: function(data) {
          updateStrongAttack(data.StrongAttack);
          updateWeakAttack(data.WeakAttack);
          updateWeakDefense(data.WeakDefense);
          updateImmunities(data.ImmuneDefense);
          updateStrongDefense(data.StrongDefense);
          changeStatus('Updated');
        }
      });
    },
    changeStatus = function(newStatus) {
      $('#statusBadge').html(newStatus);
    };

  $('#typeSelectOne').change(function (e) {
    updateValues(this.value, $('#typeSelectTwo option:selected').attr('value'));
  });
  $('#typeSelectTwo').change(function (e) {
    updateValues($('#typeSelectOne option:selected').attr('value'), this.value);
  });
  setupTypeDropdown();
});